using ShotBall.InGame;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    [SerializeField] float bounceForce = 10f; // 跳ねる強さ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ボールにだけ作用するようにする
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 垂直方向に跳ね返す（Y軸方向の速度をリセットして上に跳ばす）
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
