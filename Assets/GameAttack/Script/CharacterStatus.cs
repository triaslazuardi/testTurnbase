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
        }

        public void GetDEF() { 
            isDEF = true;
        }

        public void GetHEALTH()
        {
            SetAmountHealth(HP + HEALTH);
        }

        private void SetAmountHealth(float amount)
        {
            HP = amount;

            if (amount <= 0f)
            {
                fillBar.size = new Vector2(0, 1);
            }
            else {
                fillBar.size = new Vector2(amount / 100f, 1);
            }
        }

        public void SetIncrease() {
            if (countAction >= MaxIncreaseVal) {
                countAction = 0;

                ATK += PlusVal;
                DEF += PlusVal;
                HEALTH += PlusVal;

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

