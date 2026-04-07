using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    public class TitleBackAnimation : MonoBehaviour
    {
        [SerializeField] GameObject scaleTargetObj;
        [SerializeField] float scaleSpeed = 0.1f;
        public void StartAnimation()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("Title");
            StartCoroutine(Animation());
        }

        IEnumerator Animation()
        {
            while (scaleTargetObj.transform.localScale.x > 1f)
            {
                scaleTargetObj.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
