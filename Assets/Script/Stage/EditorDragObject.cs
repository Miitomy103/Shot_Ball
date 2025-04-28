using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace ShotBall.InGame
{
    public class EditorDragObject :DragObject
    {
        private void Awake()
        {
            GridGet();
        }
        private void Start()
        {
            if (GridData == null) Debug.LogError("gridnashi");
            if (TilemapManager.Instance.DropCheck(GridData,PivotTransform()))
            {
                TilemapManager.Instance.TileDetaChange(GridData, 1, PivotTransform());
            }
            else
            {
                Debug.LogError($"{this.GetType().Name}: オブジェクトが重なっている、または範囲外に設置されています");
            }
        }

    }
}
