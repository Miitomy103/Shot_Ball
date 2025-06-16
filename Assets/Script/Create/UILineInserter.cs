using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class UILineInserter : MonoBehaviour
    {
        [SerializeField] RectTransform ui1;              // 始点
        [SerializeField] RectTransform ui2;              // 終点
        [SerializeField] RectTransform lineImage;        // 細長い線Image

        private void Start()
        {
            InsertLineBetween();
        }
        private void Update()
        {
            InsertLineBetween();
        }
        public void InsertLineBetween()
        {
            
            Vector3 worldPos1 = ui1.position;
            Vector3 worldPos2 = ui2.position;

            // 線の位置
            Vector3 midPoint = (worldPos1 + worldPos2) / 2f;
            lineImage.position = midPoint;

            // 長さ調整
            float length = Vector3.Distance(worldPos1, worldPos2);
            lineImage.sizeDelta = new Vector2(lineImage.sizeDelta.x, length); // 横方向に伸びる前提

            // 向き調整（+90度補正）
            Vector3 direction = (worldPos2 - worldPos1).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            lineImage.rotation = Quaternion.Euler(0f, 0f, angle + 90f); // ← ここ！
        }
    }
}
