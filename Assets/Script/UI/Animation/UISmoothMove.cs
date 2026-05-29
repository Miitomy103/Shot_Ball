using System.Collections;
using UnityEngine;

public class UISmoothMove : MonoBehaviour
{
    [SerializeField] float duration = 0.5f;
    [SerializeField] Vector3 defaultTargetPosition;

    RectTransform target;
    Vector3 originalPosition;
    Coroutine currentMoveCoroutine;

    public RectTransform Target => target;

    private void Awake()
    {
        target = GetComponent<RectTransform>();
        originalPosition = target.anchoredPosition;
    }

    public void SmoothMove()
    {
        SmoothMove((Vector2)defaultTargetPosition + target.anchoredPosition);
    }

    public void SmoothMove(Vector3 destination)
    {
        StartMove(destination, duration);
    }

    public void SmoothMove(Vector3 destination, float customDuration)
    {
        StartMove(destination, customDuration);
    }

    public void ReturnToOriginalPosition()
    {
        SmoothMove(originalPosition);
    }

    void StartMove(Vector3 destination, float moveDuration)
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }

        currentMoveCoroutine = StartCoroutine(SmoothMoveCoroutine(destination, moveDuration));
    }

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
