using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame.UI
{
    public class WindUI : MonoBehaviour
    {
        public Image targetImage;
        [SerializeField] Vector2 imageOffset = new Vector2(32f, 32f); // ← インスペクターで設定推奨
        [SerializeField] WindArea windArea;

        float MoveSpeed => (windArea != null) ? windArea.WindPower / 5f : 1f;
        float between = 0.3f;

        float leftX, rightX, topY, bottomY;
        List<RectTransform> images = new List<RectTransform>();

        private void Start()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Rect rect = rectTransform.rect;

            // 正方形を前提に表示領域を取得
            leftX = rect.xMin - imageOffset.x;
            rightX = rect.xMax + imageOffset.x;
            bottomY = rect.yMin - imageOffset.y;
            topY = rect.yMax + imageOffset.y;

            StartCoroutine(Enumerator());
        }

        private void FixedUpdate()
        {
            for (int i = images.Count - 1; i >= 0; i--)
            {
                RectTransform img = images[i];
                Vector3 pos = img.localPosition;
                pos.y += MoveSpeed * Time.deltaTime;
                img.localPosition = pos;

                if (pos.y > topY)
                {
                    images.RemoveAt(i);
                    Destroy(img.gameObject);
                }
            }
        }

        private IEnumerator Enumerator()
        {
            bool gap = false;
            while (true)
            {
                float x = leftX + (gap ? imageOffset.x : 0f);
                while (x < rightX)
                {
                    Vector3 pos = new Vector3(x, bottomY, 0f);
                    GameObject obj = Instantiate(targetImage.gameObject, transform);
                    RectTransform rect = obj.GetComponent<RectTransform>();

                    // 正方形に設定（縦横固定）
                    rect.sizeDelta = new Vector2(imageOffset.x, imageOffset.x);
                    rect.localPosition = pos;

                    images.Add(rect);
                    x += between;
                }

                gap = !gap;
                yield return new WaitForSeconds(1f / MoveSpeed);
            }
        }
    }
}
