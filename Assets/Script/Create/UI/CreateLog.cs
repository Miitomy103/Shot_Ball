using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall
{
    public class CreateLog : MonoBehaviour
    {
        private static CreateLog instance;
        public static CreateLog Instance =>instance;

        [SerializeField] private Vector2 padding = new Vector2(10f, 10f); // 余白

        [SerializeField] TextMeshProUGUI logText;
        [SerializeField] RectTransform image;

        private IEnumerator hideCoroutine;
        private void Awake()
        {
            instance = this;
        }

        public void SetLog(string text)
        {
            image.gameObject.SetActive(true);
            logText.text = text;


            // Textの推奨サイズを取得
            float width = logText.preferredWidth;
            float height = logText.preferredHeight;

            // Imageのサイズを調整
            image.sizeDelta = new Vector2(width + padding.x, height + padding.y);

            StartCoroutine(HideLog());
        }

        public IEnumerator HideLog(float delay = 1f)
        {
            if (hideCoroutine != null) StopCoroutine(hideCoroutine);
            yield return new WaitForSeconds(delay);
            image.gameObject.SetActive(false);
        }
    }
}
