using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] Animator animator;

        bool isAnimation = false;
        bool isPlay = false;

        [SerializeField] Image[] laycasts;

        [SerializeField] GameObject startButton;

        public void PlayClick()
        {
            if (isAnimation) return;
            isPlay = true;
            animator.SetTrigger("Play");
            AnimationStart();
        }
        private void PlayEnd()
        {
            startButton.SetActive(false);
        }
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
