using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class EditSceneMove : MonoBehaviour
    {
        [SerializeField] string sceneName = "SaveData";
        AsyncOperation async;

        [SerializeField]float fadeSpeed = 0.1f;

        [SerializeField] Image fadeImage;
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
