using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ScaleChangeDurationManager : MonoBehaviour
    {
        [SerializeField] float minTime = 0.5f;
        [SerializeField] float maxTime = 4.5f;

        [SerializeField] RectTransform prefab;

        [SerializeField] private RectTransform parentCanvas;

        private void Start()
        {
            StartCoroutine(StartScaleChangeDuration());
        }

        private IEnumerator StartScaleChangeDuration()
        {
            while (true)
            {
                float duration = Random.Range(minTime, maxTime);
                yield return new WaitForSeconds(duration);

                Rect parentRect = parentCanvas.rect;

                float x = Random.Range(parentRect.xMin, parentRect.xMax);
                float y = Random.Range(parentRect.yMin, parentRect.yMax);

                RectTransform instance = Instantiate(prefab, this.transform);
                instance.anchoredPosition = new Vector2(x, y);
            }
        }
    }
}
