using System.Collections;
using UnityEngine;

/// <summary>
/// RectTransform.sizeDeltaを滑らかに変更するクラス
/// </summary>
//Transformを変更する場合は、TransformSmoothScaleChangerを使用してください
[RequireComponent(typeof(RectTransform))]
public class UIScaleSmoothChanger : MonoBehaviour
{
    private Vector2 originalSize;
    private RectTransform rectTransform;

    [SerializeField, Tooltip("変更する大きさの倍率")] private float targetScale = 1.1f;
    [SerializeField, Tooltip("変化する時間")] private float duration = 0.5f;

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