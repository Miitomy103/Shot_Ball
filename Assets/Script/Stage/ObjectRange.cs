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
        public Vector2 PointA => pointA;
        public Vector2 PointB => pointB;

        static ObjectRange instance;
        public static ObjectRange Instance => instance;

        [SerializeField] Transform A;
        [SerializeField] Transform B;

        private void Awake()
        {
            instance = this;
        }
        private void OnEnable()
        {
            pointA = A.position;
            pointB = B.position;
        }

        public bool InRange(Vector2 target)
        {
            Vector2 min = Vector2.Min(pointA, pointB);
            Vector2 max = Vector2.Max(pointA, pointB);
            return target.x >= min.x && target.x <= max.x &&
                   target.y >= min.y && target.y <= max.y;
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
