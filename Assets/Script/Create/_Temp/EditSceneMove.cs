using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// _Temp配下の実験用クラス。フェードしながら指定シーン(sceneName)へ遷移する。
    /// </summary>
    public class EditSceneMove : MonoBehaviour
    {
        [SerializeField] string sceneName = "SaveData";
        AsyncOperation async;

        [SerializeField]float fadeSpeed = 0.1f;

        [SerializeField] Image fadeImage;
        /// <summary>
        /// フェードしながらシーン遷移を開始する。
        /// </summary>
        public void SceneMove()
        {
            if (async != null) return;
            StartCoroutine(SceneMoveCoroutine());
        }

        IEnumerator SceneMoveCoroutine()
        {
            async = SceneManager.LoadSceneAsync(sceneName);
            async.allowSceneActivation = false;

            while (fadeImage.transform.localScale.x < 27f)
            {
                fadeImage.transform.localScale += Vector3.one * fadeSpeed * Time.deltaTime;
                yield return null;
            }

            async.allowSceneActivation = true;
        }
    }
}
