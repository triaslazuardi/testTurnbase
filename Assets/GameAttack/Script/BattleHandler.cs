using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using AttackTest.Enum;
using AttackTest.So;
using AttackTest.Character;
using UnityEngine;
using UnityEngine.Networking;

namespace AttackTest {
    public class BattleHandler : MonoBehaviour
    {
        public static BattleHandler instance;

        public DataSO dtGame;
        public UIManage scrUi;
        public UIStatusAction scrState;

        [SerializeField] private List<CharacterHandler> prefabCharPlayer;
        [SerializeField] private List<CharacterHandler> prefabCharEnemy;
        public CharacterHandler charPlayerHandle;
        public CharacterHandler charEnemyHandle;
        private CharacterHandler activeCharHandle;
        public StateAttack state;

        [Range(0, 2)]
        public int currenIdxPlayer = 0;

        [Range(0, 2)]
        public int currenIdxEnemy = 0;

        bool isProgress = false;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            state = StateAttack.Done;
            scrUi.SetPlay();
        }

        public void OnPlay() {
            if (isProgress) return;
            isProgress = true;

            playGame();
        }

        public void OnPlayAgain()
        {
            Debug.Log("IsPlayAgain : " + isProgress);
            if (isProgress) return;
            isProgress = true;

            LeanPool.Despawn(charPlayerHandle);
            LeanPool.Despawn(charEnemyHandle);

            
            playGame();
        }

        public void playGame() {
            currenIdxPlayer = 0;
            currenIdxEnemy = 0;
            charPlayerHandle = SpawnCharacter(true);
            charEnemyHandle = SpawnCharacter(false);
            SetActiveChar(charPlayerHandle);
            state = StateAttack.WaitingPlayer;
            scrUi.SetDefault();
            SoundManager.instance.PlaySFX("play");
            isProgress = false;
            scrState.UpdateTurn(true);
        }


        public CharacterHandler SpawnCharacter(bool isPlayerTeam)
        {
            Vector3 position;
            CharacterHandler charCurrent = null;

            if (isPlayerTeam)
            {
                position = new Vector3((0 - dtGame.distanceCharX), dtGame.distanceCharY);
                charCurrent = LeanPool.Spawn(prefabCharPlayer[currenIdxPlayer], position, Quaternion.identity);
            }
            else
            {
                position = new Vector3(dtGame.distanceCharX, dtGame.distanceCharY);
                charCurrent = LeanPool.Spawn(prefabCharEnemy[currenIdxEnemy], position, Quaternion.identity);
            }

            charCurrent.InitCharacter(isPlayerTeam);
            return charCurrent;
        }

        private void SetActiveChar(CharacterHandler characterHandler) {
            activeCharHandle = characterHandler;
        }

        public void ChooseNextChar()
        {
            if (state == StateAttack.Done) return;

            if (BattleOver())
            {
                state = StateAttack.Done;
                SoundManager.instance.PlaySFX("end");
                return;
            }

            if (activeCharHandle == charPlayerHandle)
            {
                //scrState.UpdateTurn(false);
                SetActiveChar(charEnemyHandle);
                state = StateAttack.Progress;

                EnemyTurn();
            }
            else {
                scrState.UpdateTurn(true);
                SetActiveChar(charPlayerHandle);
                state = StateAttack.WaitingPlayer;
            }
        }

        private void EnemyTurn() {
            int decideAction = 0;

            if (charEnemyHandle.lastAction == 1 && charEnemyHandle.healthCharacter.GetValHP() >= 50)
            {
                decideAction = Random.Range(0, 2) == 0 ? 0 : 2;
            }

            if (charEnemyHandle.lastAction == 2)
            {
                decideAction = Random.Range(0, 2);
            }

            if (charEnemyHandle.healthCharacter.GetValHP() < 30f) 
            {
                decideAction = Random.Range(0, 10) < 7 ? 1 : 0;
            }
            else if (charPlayerHandle.lastAction == 2) {
                decideAction = Random.Range(0, 10) < 8 ? 0 : 2;
            }else 
            {
                int action = Random.Range(0, 10);
                if (action >= 3 && action < 5) decideAction = 2;   
                else if (action >= 5 && action < 7 ) decideAction = 1;   
                else decideAction = 0;

                Debug.Log("Decide action : " + action);
            }

            Debug.Log("Decide : " + decideAction);

            switch (decideAction) { 
                case 0:
                    charEnemyHandle.AttackAction(charPlayerHandle, () => {
                        scrState.UpdateTextActionEnemy("");
                        ChooseNextChar();
                    });
                    scrState.UpdateTextActionEnemy("Attack");
                    break;
                case 1:
                    charEnemyHandle.HealthAction(charEnemyHandle, () => {
                        scrState.UpdateTextActionEnemy("");
                        ChooseNextChar();
                    });
                    scrState.UpdateTextActionEnemy("Recovery");
                    break;
                case 2:
                    charEnemyHandle.DefenseAction(charEnemyHandle, () => {
                        scrState.UpdateTextActionEnemy("");
                        ChooseNextChar();
                    });
                    scrState.UpdateTextActionEnemy("Defense");
                    break;
            }

            

            
        }

        private bool BattleOver() { 
            if (charPlayerHandle.IsDie()) {
                charPlayerHandle.SetDie();
                scrUi.SetWinner(false);
                return true;
            }

            if (charEnemyHandle.IsDie()) {
                charEnemyHandle.SetDie();
                scrUi.SetWinner(true);
                return true;
            }

            return false;
        }
    }
}

