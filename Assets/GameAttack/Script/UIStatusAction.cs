using AttackTest.Enum;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AttackTest
{
    public class UIStatusAction : MonoBehaviour
    {
        [Space]
        [SerializeField] private Image imgTurnPlayer;
        [SerializeField] private Image imgTurnEnemy;

        [Space][Header("StatePlayer")]
        [SerializeField] private TMP_Text textHP_Player;
        [SerializeField] private TMP_Text textATK_Player;
        [SerializeField] private TMP_Text textDEF_Player;
        [SerializeField] private TMP_Text textHEALTH_Player;

        [Space][Header("StateEnemy")]
        [SerializeField] private TMP_Text textHP_Enemy;
        [SerializeField] private TMP_Text textATK_Enemy;
        [SerializeField] private TMP_Text textDEF_Enemy;
        [SerializeField] private TMP_Text textHEALTH_Enemy;


        public void UpdateTextHP(bool isPlayer, int currentVal, int maxVal) {
            if (isPlayer)
            {
                textHP_Player.text = $"{currentVal}/{maxVal}";
            }
            else {
                textHP_Enemy.text = $"{currentVal}/{maxVal}";
            }
        }

        public void UpdateTextATK(bool isPlayer, int currentVal)
        {
            if (isPlayer)
            {
                textATK_Player.text = $"{currentVal}";
            }
            else
            {
                textATK_Enemy.text = $"{currentVal}";
            }
        }

        public void UpdateTextDEF(bool isPlayer, int currentVal)
        {
            if (isPlayer)
            {
                textDEF_Player.text = $"{currentVal}";
            }
            else
            {
                textDEF_Enemy.text = $"{currentVal}";
            }
        }

        public void UpdateTextHEALTH(bool isPlayer, int currentVal)
        {
            if (isPlayer)
            {
                textHEALTH_Player.text = $"{currentVal}";
            }
            else
            {
                textHEALTH_Enemy.text = $"{currentVal}";
            }
        }

        public void OnActionAttack()
        {
            if (BattleHandler.instance.state != StateAttack.WaitingPlayer) return;
            BattleHandler.instance.state = StateAttack.Progress;

            BattleHandler.instance.charPlayerHandle.AttackAction(BattleHandler.instance.charEnemyHandle, () => {
                BattleHandler.instance.ChooseNextChar();
            });
        }

        public void OnActionDEF()
        {
            if (BattleHandler.instance.state != StateAttack.WaitingPlayer) return;
            BattleHandler.instance.state = StateAttack.Progress;

            BattleHandler.instance.charPlayerHandle.DefenseAction(() => {
                BattleHandler.instance.ChooseNextChar();
            });
        }

        public void OnActionHEALER()
        {
            if (BattleHandler.instance.state != StateAttack.WaitingPlayer) return;
            BattleHandler.instance.state = StateAttack.Progress;

            BattleHandler.instance.charPlayerHandle.HealthAction(() => {
                BattleHandler.instance.ChooseNextChar();
            });
        }
    }
}