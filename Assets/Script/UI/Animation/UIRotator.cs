using UnityEngine;

/// <summary>
/// 一定速度でZ軸回転を続けるクラス。
/// </summary>
public class UIRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f; // 度/秒

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}