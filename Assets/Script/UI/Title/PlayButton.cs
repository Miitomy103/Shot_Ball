using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    /// <summary>
    /// タイトル画面のプレイ/バックボタン操作とアニメーション制御を行うクラス。
    /// </summary>
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] Animator animator;

        bool isAnimation = false;
        bool isPlay = false;

        [SerializeField] Image[] laycasts;

        [SerializeField] GameObject startButton;

        private void Start()
        {
            if(StaticData.isSelectView)
            {
                animator.Play("Play",0,1);
                animator.Update(0);
                PlayEnd();
                StaticData.isSelectView = false;
            }
        }
        /// <summary>
        /// プレイボタンが押されたときの処理。ステージ選択へのアニメーションを開始する。
        /// </summary>
        public void PlayClick()
        {
            if (isAnimation) return;
            Debug.Log("PlayClick");
            isPlay = true;
            animator.SetTrigger("Play");
            AnimationStart();
        }
        private void PlayEnd()
        {
            startButton.SetActive(false);
        }
        /// <summary>
        /// バックボタンが押されたときの処理。タイトルへ戻るアニメーションを開始する。
        /// </summary>
        public void BackClick()
        {
            if (isAnimation) return;
            isPlay = false;
            animator.SetTrigger("Back");
            AnimationStart();
            startButton.SetActive(true);
        }
        private void BackEnd()
        {
            
        }

        /// <summary>
        /// ボタン操作を無効化し、アニメーション開始の待機処理を行う。
        /// </summary>
        public void AnimationStart()
        {
            foreach (var item in laycasts)
            {
                item.raycastTarget = false;
            }
            isAnimation = true;
            StartCoroutine(WaitForFrame());
        }

        IEnumerator WaitForFrame()
        {
            yield return new WaitForSeconds(0.5f);
            AnimationEnd();
        }
        /// <summary>
        /// アニメーション終了時の処理。ボタン操作を再度有効化する。
        /// </summary>
        public void AnimationEnd()
        {
            foreach (var item in laycasts)
            {
                item.raycastTarget = true;
            }

            isAnimation = false;

            if(isPlay)PlayEnd();
            else BackEnd();
        }
    }
}
