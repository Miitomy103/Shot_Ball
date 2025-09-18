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
            if(async != null)
            {
                return;
            }
            StartCoroutine(LoadCoroutine());
            StartCoroutine(OnClick());
        }
        IEnumerator LoadCoroutine()
        {
            async = SceneManager.LoadSceneAsync(sceneName);
            async.allowSceneActivation = false; // Å© Ç±ÇÃéûì_Ç≈ÇÕÇ‹Çæà⁄ìÆÇµÇ»Ç¢
            yield return async;
        }
        IEnumerator OnClick()
        {
            while(fadeImage.transform.localScale.x < 27f)
            {
                fadeImage.transform.localScale += new Vector3(fadeSpeed,fadeSpeed,fadeSpeed);
                yield return null;
            }
            async.allowSceneActivation = true; // Å© Ç±Ç±Ç≈ÉVÅ[ÉìêÿÇËë÷Ç¶î≠ìÆ
        }
    }
}
