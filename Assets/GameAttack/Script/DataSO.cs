using System.Collections;
using System.Collections.Generic;
using AttackTest.Character;
using UnityEngine;

namespace AttackTest.So {
    [CreateAssetMenu(fileName = "Custom Item SO", menuName = "Scriptable Object/Game Default Data")]
    public class DataSO : ScriptableObject
    {
        [Header("ValueAttribute")]
        public int distanceCharX = 0;
        public int distanceCharY = 0;
        public int slideSpeed = 0;
        public float reachedDistance = 0;
        public float rangeDistance = 0;
        public Vector3 vecFx;

        [Space]
        [SerializeField] private List<int> damageVal;
        [SerializeField] private List<float> defVal; 
        [SerializeField] private List<float> healthVal;
        [SerializeField] private List<float> increaseVal;


        [Space][Header("DelayNumber")]
        public float delay1 = 0.125f;

        [Header("Fx")]
        public FxManage fxBlood = null;


        public float GetRandomeDamage() {
            return (float)damageVal[UnityEngine.Random.Range(0, damageVal.Count)];
        }

        public float GetRandomeDEF() {
            return defVal[UnityEngine.Random.Range(0, defVal.Count)];
        }

        public float GetRandomeHealth()
        {
            return healthVal[UnityEngine.Random.Range(0, healthVal.Count)];
        }

        public float GetRandomeIncrease()
        {
            return increaseVal[UnityEngine.Random.Range(0, increaseVal.Count)];
        }
    }
}

