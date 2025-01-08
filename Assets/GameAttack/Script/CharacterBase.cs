using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AttackTest.Character {
    public class CharacterBase : MonoBehaviour
    {
        [SerializeField] public Animator animCharacter = null;
        [SerializeField] public float durationAttact = 0f;

        public void CharacterIdle()
        {
            //ResetAllAnimatorTriggers();
            animCharacter.SetTrigger("idle");
        }
        public void CharacterRun()
        {
            animCharacter.SetTrigger("run");
        }
        public void CharacterDie()
        {
            //ResetAllAnimatorTriggers();
            animCharacter.SetTrigger("die");
        }

        private void CharacterAttackType1()
        {
            animCharacter.SetTrigger("attack_1");

        }
        private void CharacterAttackType2()
        {
            //ResetAllAnimatorTriggers();
            animCharacter.SetTrigger("attack_2");
        }

        public void CHaracterAttact(Vector3 dirAttack, UnityAction callbackDamage , UnityAction callback) {
            CharacterAttackType2();
            StartCoroutine(WaitCharacterAttack(callbackDamage, callback));
            //float waitTime = animCharacter.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            
        }

        private IEnumerator WaitCharacterAttack(UnityAction callbackDamage, UnityAction callback) {
            yield return new WaitForSeconds(durationAttact - BattleHandler.instance.dtGame.delay1);
            callbackDamage?.Invoke();
            yield return new WaitForSeconds(BattleHandler.instance.dtGame.delay1);
            callback?.Invoke();
        }

        public void ResetAllAnimatorTriggers()
        {
            foreach (var trigger in animCharacter.parameters)
            {
                if (trigger.type == AnimatorControllerParameterType.Trigger)
                {
                    animCharacter.ResetTrigger(trigger.name);
                }
            }
        }

    }
}


