using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.UI
{
    /// <summary>
    /// タイトル画面の背景アニメーションを制御するクラス。
    /// </summary>
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
                scaleTargetObj.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
