using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShotBall.InGame
{
    public abstract class GimmickBase : MonoBehaviour
    {
        protected virtual Vector2 Direction { get; }

        protected Quaternion DirectionAngle()
        {
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(0, 0, angle - 90f);
        }

        void OnDrawGizmos()
        {
            Vector3 start = transform.position;
            Vector3 dir3D = new Vector3(Direction.x, Direction.y, 0).normalized;
            Vector3 end = start + dir3D * 0.5f;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, end);

            // ñÓàÛÇÃêÊí[Çï`Ç≠ÅiéOäpÇÃâHÅj
            Vector3 right = Quaternion.Euler(0, 0, 150) * dir3D;
            Vector3 left = Quaternion.Euler(0, 0, -150) * dir3D;

            Gizmos.DrawLine(end, end + right * 0.2f);
            Gizmos.DrawLine(end, end + left * 0.2f);
        }
    }
}
