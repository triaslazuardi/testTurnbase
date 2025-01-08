using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AttackTest {

    public class UIManage : MonoBehaviour
    {
        [SerializeField] private GameObject objWinLose;
        [SerializeField] private GameObject objPlay;
        [SerializeField] private GameObject objBlock;


        [SerializeField] private TMP_Text txtWinLose;
        [SerializeField] private CanvasGroup canvasUI;

        public void SetWinner(bool isPlayer) {
            if (isPlayer)
            {
                txtWinLose.text = "Player Wins";
            }
            else {
                txtWinLose.text = "Player Lose";
            }
            objWinLose.SetActive(true);
            AnimateCanvas();
        }

        public void SetPlay()
        {
            objPlay.SetActive(true);
            objWinLose.SetActive(false);
            AnimateCanvas();
        }

        public void SetDefault() {
            canvasUI.DOFade(0, BattleHandler.instance.dtGame.delay1).OnComplete(() => {
                objPlay.SetActive(false);
                objWinLose.SetActive(false);
                canvasUI.alpha = 0f;
                canvasUI.blocksRaycasts = false;
                OperateBlock(false);
            });
        }

        public void AnimateCanvas() {
            canvasUI.DOFade(1, BattleHandler.instance.dtGame.delay1 * 2);
            canvasUI.blocksRaycasts = true;
        }

        public void OperateBlock(bool isActive) {
            objBlock.SetActive(isActive);
        }


        public void OnClickPlay(bool isAgain)
        {
            if (isAgain) {
                BattleHandler.instance.OnPlayAgain();
            }
            else
            {
                BattleHandler.instance.OnPlay();
            }
        }

    }

}


