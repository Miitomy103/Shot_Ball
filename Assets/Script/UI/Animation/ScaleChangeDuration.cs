using UnityEngine;

/// <summary>
/// ランダムな速度で徐々に拡大し、最大スケールに達したら自身を破棄するクラス。
/// </summary>
public class ScaleChangeDuration : MonoBehaviour
{
    [SerializeField] float minSpeed = 20f;
    [SerializeField] float maxSpeed = 30f;
    [SerializeField] float maxScale = 50f;
    void Update()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        transform.localScale += Vector3.one * speed * Time.deltaTime;
        if(transform.localScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }
}
