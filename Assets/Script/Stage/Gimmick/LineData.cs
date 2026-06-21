using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// LineManagerが管理する1本のラインの情報(起点・終点・LineRenderer・所属オブジェクト)。
    /// </summary>
    public class LineData
    {
        public Transform start;     // 起点
        public Transform end;       // 終点
        public LineRenderer renderer;
        public GameObject obj;

        public LineData(Transform start, Transform end, LineRenderer renderer, GameObject obj)
        {
            this.start = start;
            this.end = end;
            this.renderer = renderer;
            this.obj = obj;
        }
    }

}
