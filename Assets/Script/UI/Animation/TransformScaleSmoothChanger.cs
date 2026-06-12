using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class TransformSmoothScaleChanger : MonoBehaviour
    {
        private Vector3 originalScale;

        [SerializeField] private float targetScale = 1.1f;
        [SerializeField] private float duration = 0.5f;

        private void Start()
        {
            originalScale = transform.localScale;
        }

        public void StartDefaultScaleChange()
        {
            StartCoroutine(SmoothScaleChange(targetScale, duration));
        }

        public void StartDefaultScaleReset()
        {
            StartCoroutine(SmoothScaleReset(duration));
        }

        public IEnumerator SmoothScaleChange(float targetScale, float duration)
        {
            Vector3 initialScale = transform.localScale;
            Vector3 targetScaleVector = originalScale * targetScale;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);

                transform.localScale = Vector3.Lerp(initialScale, targetScaleVector, t);

                yield return null;
            }

            transform.localScale = targetScaleVector;
        }

        public IEnumerator SmoothScaleReset(float duration)
        {
            Vector3 initialScale = transform.localScale;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);

                transform.localScale = Vector3.Lerp(initialScale, originalScale, t);

                yield return null;
            }

            transform.localScale = originalScale;
        }
    }
}
