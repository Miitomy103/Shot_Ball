using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class TutorialManager : MonoBehaviour
    {
        readonly string[] animationName= new string[3]
        {
            "Move",
            "Rotation",
            "Click"
        };
        bool[] isClear = new bool[3]
        {
            false,
            false,
            false
        };
        [SerializeField] Animator animator;
        [SerializeField] int tutorialCount = 0;

        [SerializeField] RectTransform rectTransform;
        [SerializeField] Vector2 rotationPos;
        [SerializeField] Vector2 clickPos;
        private void Awake()
        {
            rectTransform=animator.GetComponent<RectTransform>();
        }
        private void Start()
        {
            GameLoop.Instance.StartAction += ClickClear;
            ClearTutorial();
        }
        public void MoveClear()
        {
            isClear[0] = true;
            ClearTutorial();
        }
        public void RotationClear()
        {
            isClear[1] = true;
            ClearTutorial();
        }
        public void ClickClear()
        {
            isClear[2] = true;
            ClearTutorial();
        }
        public void ClearTutorial()
        {
            int c = 0;
            bool b = false;
            for(int i = 0; i < isClear.Length; i++)
            {
                if (!isClear[i])
                {
                    c = i;
                    b = true;
                    break;
                }
            }
            if (!b)
            {
                animator.gameObject.SetActive(false);
                return;
            }
            animator.SetTrigger(animationName[c]);
            Debug.Log("Tutorial Clear: " + animationName[c]);
            if(c == 1)
            {
                StartCoroutine(Coroutine(rotationPos));
                Debug.Log("Set Position: " + rotationPos);
            }
            else if (c == 2)
            {
                StartCoroutine(Coroutine(clickPos));
                Debug.Log("Set Position: " + clickPos);
            }
        }
        IEnumerator Coroutine(Vector2 vector2)
        {
            yield return new WaitForSeconds(1f);
            rectTransform.anchoredPosition = vector2;
        }
    }

    
}
