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

        //[Space][Header"BuutonAction"]

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