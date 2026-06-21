using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame.UI
{
    /// <summary>
    /// 風で吹き上がる演出のため、画像タイルを下端から生成して上端まで流すクラス。
    /// </summary>
    public class WindUI : MonoBehaviour
    {
        public Image targetImage;

        [SerializeField] Vector2 imageOffset;
        float moveSpeed = 1;
        float between = 0.3f;

        
         Vector3 leftEdge;
         Vector3 rightEdge;
         float upEdge;
         float downEdge;

        List<RectTransform> images = new List<RectTransform>();
        private void Start()
        {

            RectTransform rectTransform = GetComponent<RectTransform>();

            GameObject pare = transform.parent.gameObject;
            RectTransform parentRect = pare.GetComponent<RectTransform>();
            rectTransform.sizeDelta = parentRect.sizeDelta * 2;


            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            // 親オブジェクトのローカル座標に変換
            Transform parent = transform;

            // 左下と右下を親基準のローカル座標に
            leftEdge = parent.InverseTransformPoint(worldCorners[0]);  // 左下
            rightEdge = parent.InverseTransformPoint(worldCorners[3]); // 右下

            imageOffset = targetImage.rectTransform.sizeDelta;

            leftEdge += new Vector3(-imageOffset.x, 0);
            rightEdge += new Vector3(imageOffset.x, 0);
            downEdge = rightEdge.y - imageOffset.y;

            Vector3 topLeft = parent.InverseTransformPoint(worldCorners[1]); // 左上
            upEdge = topLeft.y + imageOffset.y;



            StartCoroutine(Enumerator());
        }

        private void FixedUpdate()
        {
            if (images.Count == 0) return;

            for (int j = images.Count - 1; j >= 0; j--)
            {
                var i = images[j];
                Vector3 v = i.localPosition;
                v.y += moveSpeed * Time.deltaTime;
                i.localPosition = v;

                if (v.y > upEdge)
                {
                    images.RemoveAt(j);
                    Destroy(i.gameObject);
                }
            }
        }

        private IEnumerator Enumerator()
        {
            bool gap = false;
            while (true)
            {
                float x = leftEdge.x + (gap ? imageOffset.x : 0);
                while (x < rightEdge.x)
                {
                    Vector3 pos = new Vector3(x, downEdge, 0f);
                    GameObject obj = Instantiate(targetImage.gameObject, transform);
                    obj.transform.localPosition = pos;
                    images.Add(obj.GetComponent<RectTransform>());

                    x += between;
                }
                gap = !gap;
                yield return new WaitForSeconds(0.5f);
            }
        }

    }
}
