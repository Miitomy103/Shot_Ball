using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class DragObject : MonoBehaviour
    {
        [SerializeField]protected Vector2 pivot;

        //0 => 空
        //1 => マス
        //2 => 中心地
        protected int[,] GridData { get;private set; }


        protected void GridGet()
        {
            var g = GetComponent<IGridData>();

            GridData = g.GridData;
        }
        

        protected Vector3 PivotTransform()
        {
            return new Vector3(transform.position.x - pivot.x, transform.position.y - pivot.y + 0);
        }

        protected void OnDrawGizmos()
        {
            float size = 0.25f;

            Gizmos.color = Color.red;

            Vector3 pos = PivotTransform();

            // 横線
            Gizmos.DrawLine(pos + Vector3.left * size, pos + Vector3.right * size);
            // 縦線
            Gizmos.DrawLine(pos + Vector3.up * size, pos + Vector3.down * size);
        }
    }

}
