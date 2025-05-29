using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class LineManager : CollisionBase
    {
        protected abstract Transform StartPoint { get; }
        private readonly float lineWidth = 0.05f;

        protected List<LineData> lineDatas = new List<LineData>();

        // ラインを作成する
        protected void CreateLine(Transform endPoint)
        {
            GameObject lineObj = new GameObject("Line");
            lineObj.transform.parent = transform;
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, StartPoint.position);
            lr.SetPosition(1, endPoint.position);
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;

            LineData data = new LineData(StartPoint, endPoint, lr, lineObj);

            lineDatas.Add(data);
        }
        protected void ChangePosition(int index)
        {
            if (lineDatas[index].obj.activeSelf && lineDatas[index].start != null && lineDatas[index].end != null)
            {
                lineDatas[index].renderer.SetPosition(0, lineDatas[index].start.position);
                lineDatas[index].renderer.SetPosition(1, lineDatas[index].end.position);
            }
        }
        // ラインの表示切替
        protected void SetLineActive(int index, bool isActive)
        {
            if (index >= 0 && index < lineDatas.Count)
            {
                lineDatas[index].obj.SetActive(isActive);
            }
            if(isActive)
            {
                ChangePosition(index);
            }
        }

        // すべてのラインをオフ
        protected void DisableAllLines()
        {
            foreach (var data in lineDatas)
            {
                data.obj.SetActive(false);
            }
        }

        // すべてのラインをオン
        protected void EnableAllLines()
        {
            foreach (var data in lineDatas)
            {
                data.obj.SetActive(true);
            }
        }
    }

}
