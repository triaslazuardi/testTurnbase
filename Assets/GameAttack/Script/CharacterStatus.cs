using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackTest.Character {
    public class CharacterStatus : MonoBehaviour
    {
        [SerializeField]protected float HP = 0;
        [SerializeField]protected float ATK;
        [SerializeField]protected float DEF;
        [SerializeField]protected float HEALTH;
        [SerializeField]protected float PlusVal;

        public int MaxIncreaseVal = 5;
        public int countAction = 0;

        public SpriteRenderer fillBar;
        public bool isDEF = false;
        public bool ispLayer = false;

        public void Operate(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void InitHelath(float _HP, float _ATK, float _DEF, float _HEALTH, float _PlusVal) {
            SetAmountHealth(_HP);
            Operate(true);

            ATK = _ATK;
            DEF = _DEF;
            HEALTH = _HEALTH;
            PlusVal = _PlusVal;

            BattleHandler.instance.scrState.UpdateTextATK(ispLayer, (int)ATK);
            BattleHandler.instance.scrState.UpdateTextDEF(ispLayer, (int)DEF);
            BattleHandler.instance.scrState.UpdateTextHEALTH(ispLayer, (int)HEALTH);
        }

        public void GetDamage(float amount)
        {
            if(amount <= 0) return;

            if (isDEF) {
                amount -= DEF;

                if (amount <= 0) amount = 0;

                isDEF = false;
            }

            SetAmountHealth(HP-amount);

            SetIncrease();
        }

        public void GetDEF() { 
            isDEF = true;
            SoundManager.instance.PlaySFX("defend");
            SetIncrease();
        }

        public void GetHEALTH()
        {
            SetAmountHealth(HP + HEALTH);
            SoundManager.instance.PlaySFX("heal");
            SetIncrease();
        }

        private void SetAmountHealth(float amount)
        {
            HP = amount;

            if (amount <= 0f)
            {
                fillBar.size = new Vector2(0, 1);
                BattleHandler.instance.scrState.UpdateTextHP(ispLayer, 0, 100);
            }
            else {
                if (HP > 100f) { 
                    HP = 100f;
                }

                BattleHandler.instance.scrState.UpdateTextHP(ispLayer, (int)HP, 100);
                fillBar.size = new Vector2(amount / 100f, 1);
            }
        }

        public void SetIncrease() {
            Debug.Log("increase : "+ countAction);
            if (countAction >= MaxIncreaseVal) {
                countAction = 0;
                Debug.Log("masukkkk : " + countAction);
                ATK += PlusVal;
                DEF += PlusVal;
                HEALTH += PlusVal;

                BattleHandler.instance.scrState.UpdateTextATK(ispLayer, (int)ATK);
                BattleHandler.instance.scrState.UpdateTextDEF(ispLayer, (int)DEF);
                BattleHandler.instance.scrState.UpdateTextHEALTH(ispLayer, (int)HEALTH);

                return;
            }

            countAction++;
        }


        #region getStatus
        public float GetValHP()
        {
            return HP;
        }
        public float GetValATK()
        {
            return ATK;
        }
        public float GetValDEF()
        {
            return DEF;
        }
        public float GetValHealth() {
            return HEALTH;
        }
        #endregion
    }
}

