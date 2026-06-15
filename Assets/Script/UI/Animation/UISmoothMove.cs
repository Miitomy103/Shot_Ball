using System.Collections;
using UnityEngine;

/// <summary>
/// このコンポーネントをアタッチしたUIオブジェクトを、RectTransformのanchoredPositionを補間して移動させます。
/// </summary>
public class UISmoothMove : MonoBehaviour
{
    [Tooltip("移動にかける秒数です。0以下の場合は即座に移動します。")]
    [SerializeField] float duration = 0.5f;

    [Tooltip("UnityEventからSmoothMove()を呼んだ時に、現在位置へ加算する移動量です。")]
    [SerializeField] Vector3 defaultTargetPosition;

    RectTransform target;
    Vector3 originalPosition;
    Coroutine currentMoveCoroutine;

    /// <summary>
    /// このコンポーネントが移動させるRectTransformを取得します。
    /// </summary>
    public RectTransform Target => target;

    /// <summary>
    /// 移動対象のRectTransformと、元のanchoredPositionを記録します。
    /// </summary>
    private void Awake()
    {
        target = GetComponent<RectTransform>();
        originalPosition = target.anchoredPosition;
    }

    /// <summary>
    /// defaultTargetPositionを移動量として、現在のanchoredPositionから相対移動します。
    /// UnityEventから引数なしで呼ぶための関数です。
    /// </summary>
    public void SmoothMove()
    {
        SmoothMove((Vector2)defaultTargetPosition + target.anchoredPosition);
    }

    /// <summary>
    /// 指定したanchoredPositionへ、Inspectorのdurationで移動します。
    /// </summary>
    /// <param name="destination">移動先のanchoredPosition。</param>
    public void SmoothMove(Vector3 destination)
    {
        StartMove(destination, duration);
    }

    /// <summary>
    /// 指定したanchoredPositionへ、指定した秒数で移動します。
    /// </summary>
    /// <param name="destination">移動先のanchoredPosition。</param>
    /// <param name="customDuration">移動にかける秒数。</param>
    public void SmoothMove(Vector3 destination, float customDuration)
    {
        StartMove(destination, customDuration);
    }

    /// <summary>
    /// Awake時点で記録した元のanchoredPositionへ戻します。
    /// </summary>
    public void ReturnToOriginalPosition()
    {
        SmoothMove(originalPosition);
    }

    /// <summary>
    /// 実行中の移動を停止してから、新しい移動を開始します。
    /// </summary>
    /// <param name="destination">移動先のanchoredPosition。</param>
    /// <param name="moveDuration">移動にかける秒数。</param>
    void StartMove(Vector3 destination, float moveDuration)
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        currentMoveCoroutine = StartCoroutine(SmoothMoveCoroutine(destination, moveDuration));
    }

    /// <summary>
    /// 現在位置から指定位置までRectTransformを補間して移動します。
    /// </summary>
    /// <param name="destination">移動先のanchoredPosition。</param>
    /// <param name="duration">移動にかける秒数。</param>
    IEnumerator SmoothMoveCoroutine(Vector3 destination, float duration)
    {
        Vector3 startPosition = target.anchoredPosition;
        float elapsedTime = 0f;
        if (duration <= 0f)
        {
            target.anchoredPosition = destination;
            currentMoveCoroutine = null;
            yield break;
        }

        while (elapsedTime < duration)
        {
            target.anchoredPosition = Vector3.Lerp(startPosition, destination, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.anchoredPosition = destination;
        currentMoveCoroutine = null;
    }
}
