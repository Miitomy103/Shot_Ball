using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIScaleSmoothChanger : MonoBehaviour
{
    private Vector2 originalSize;
    private RectTransform rectTransform;

    [SerializeField] private float targetScale = 1.1f;
    [SerializeField] private float duration = 0.5f; 

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
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
        Vector2 initialSize = rectTransform.sizeDelta;
        Vector2 targetSize = originalSize * targetScale;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            rectTransform.sizeDelta = Vector2.Lerp(initialSize, targetSize, t);

            yield return null;
        }

        rectTransform.sizeDelta = targetSize;
    }

    public IEnumerator SmoothScaleReset(float duration)
    {
        Vector2 initialSize = rectTransform.sizeDelta;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            rectTransform.sizeDelta = Vector2.Lerp(initialSize, originalSize, t);

            yield return null;
        }

        rectTransform.sizeDelta = originalSize;
    }
}