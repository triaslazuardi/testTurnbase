using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using AttackTest.Enum;
using UnityEngine;
using UnityEngine.Events;

namespace AttackTest.Character {
    public class CharacterHandler : MonoBehaviour
    {
        [SerializeField] private CharacterBase baseCharacter;
        [SerializeField] private CharacterStatus healthCharacter;
        [SerializeField] private Transform transformCharacter;
        [SerializeField] private Transform transformSign;

        private StateSliding state;
        private Vector3 slideTargetPosition;
        private UnityAction onSlideComplete;

        public bool IsPlayer = false;

        private void Update()
        {
            switch (state)
            {
                case StateSliding.Idle:
                    break;
                case StateSliding.Progress:
                    break;
                case StateSliding.Sliding:
                    transform.position += (slideTargetPosition-GetPosition()) * BattleHandler.instance.dtGame.slideSpeed * Time.deltaTime;

                    Debug.Log("[Distance] " + Vector3.Distance(GetPosition(), slideTargetPosition) + ", reach : " + BattleHandler.instance.dtGame.reachedDistance);
                    if (Vector3.Distance(GetPosition(), slideTargetPosition) <= BattleHandler.instance.dtGame.reachedDistance) {
                        transform.position = slideTargetPosition;
                        onSlideComplete?.Invoke();
                    }
                    break;
            }
        }

        public void InitCharacter(bool _isplayer)
        {
            IsPlayer = _isplayer;
            baseCharacter.CharacterIdle();
            state = StateSliding.Idle;
            SetupScaleRotation();
            healthCharacter.InitHelath(
                BattleHandler.instance.dtGame.GetDefaultHP(),
                BattleHandler.instance.dtGame.GetRandomeDamage(),
                BattleHandler.instance.dtGame.GetRandomeDEF(),
                BattleHandler.instance.dtGame.GetRandomeHealth(),
                BattleHandler.instance.dtGame.GetRandomeIncrease()
                );
        }

        public void SetupScaleRotation() {
            if (IsPlayer)
            {
                transformCharacter.localScale = Vector3.one;
            }
            else {
                transformCharacter.localScale = new Vector3(-1,1,1);
            }
        }

        public void SetDie()
        {
            baseCharacter.CharacterDie();
            healthCharacter.Operate(false);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool IsDie()
        {
            return (healthCharacter.GetValHP() <= 0);
        }

        public void AttackAction(CharacterHandler targetChar, UnityAction OnAttackComplete = null) {
            Vector3 slideTargetposition = targetChar.GetPosition() + (GetPosition() - targetChar.GetPosition()).normalized * BattleHandler.instance.dtGame.rangeDistance;
            Vector3 startingPosition = GetPosition();
            transformSign.gameObject.SetActive(true);

            SlideToPosition(slideTargetposition, () =>
            {
                state = StateSliding.Progress;
                Vector3 attactDir = (targetChar.GetPosition() - GetPosition().normalized);
                baseCharacter.CHaracterAttact(attactDir,() => {
                    var fxItem = LeanPool.Spawn(BattleHandler.instance.dtGame.fxBlood, targetChar.transform);
                    fxItem.transform.localPosition = BattleHandler.instance.dtGame.vecFx;
                    fxItem.RunFx();
                    //targetChar.healthCharacter.GetDamage(BattleHandler.instance.dtGame.GetRandomeDamage());
                    targetChar.healthCharacter.GetDamage(healthCharacter.GetValATK());
                    SoundManager.instance.PlaySFX("attack");
                }, () =>
                {
                    SlideToPosition(startingPosition, () =>
                    {
                        state = StateSliding.Idle;
                        baseCharacter.CharacterIdle();
                        transformSign.gameObject.SetActive(false);
                        OnAttackComplete?.Invoke();
                    });
                });
            });
        }

        public void DefenseAction(UnityAction OnAttackComplete = null) {
            healthCharacter.GetDEF();
            OnAttackComplete?.Invoke();
        }

        public void HealthAction(UnityAction OnAttackComplete = null)
        {
            healthCharacter.GetHEALTH();
            OnAttackComplete?.Invoke();
        }

        private void SlideToPosition(Vector3 _targetPos, UnityAction _OnSlideComplete) { 
            this.slideTargetPosition = _targetPos;
            this.onSlideComplete = _OnSlideComplete;
            state = StateSliding.Sliding;
        }
    }
}

