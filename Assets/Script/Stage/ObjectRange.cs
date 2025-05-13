using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ShotBall.InGame
{
    public class ObjectRange : MonoBehaviour
    {
        Vector2 pointA;
        Vector2 pointB;

        static ObjectRange instance;
        public static ObjectRange Instance => instance;

        private void Awake()
        {
            instance = this;
        }
        private void OnValidate()
        {
            instance = this;
        }

        public void CameraSizeChange(Vector2 A,Vector2 B)
        {
            pointA = A;
            pointB = B;
        }

        public bool InRange(SpriteRenderer sprite)
        {
            if (sprite == null) return false; // スプライトがnullの場合はfalseを返す

            // 範囲の最小・最大点を計算
            Vector2 min = Vector2.Min(pointA, pointB);
            Vector2 max = Vector2.Max(pointA, pointB);

            // Spriteの境界（bounds）を取得
            Bounds spriteBounds = sprite.bounds;

            // Spriteの境界の最小・最大点を計算
            Vector2 spriteMin = spriteBounds.min;
            Vector2 spriteMax = spriteBounds.max;

            // Spriteの境界全体が範囲内にあるかを判定
            return spriteMin.x >= min.x && spriteMax.x <= max.x &&
                   spriteMin.y >= min.y && spriteMax.y <= max.y;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector2 min = Vector2.Min(pointA, pointB);
            Vector2 max = Vector2.Max(pointA, pointB);

            Vector3 topLeft = new Vector3(min.x, max.y, 0);
            Vector3 topRight = new Vector3(max.x, max.y, 0);
            Vector3 bottomRight = new Vector3(max.x, min.y, 0);
            Vector3 bottomLeft = new Vector3(min.x, min.y, 0);

            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
    }
}
