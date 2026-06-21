using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// このオブジェクトから複数のendPointへ向かうLineRendererを生成・管理するクラス。
    /// OnOffBlockなどで接続先を視覚的に示すために使う。
    /// </summary>
    public class LineManager : MonoBehaviour
    {
        [SerializeField] float lineWidth = 0.05f;

        protected List<LineData> lineDatas = new List<LineData>();
        public LineData[] LineDatas => lineDatas.ToArray();

        // ラインを作成する
        public void CreateLine(Transform endPoint)
        {
            GameObject lineObj = new GameObject("Line");
            lineObj.transform.parent = transform;
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, endPoint.position);
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;

            LineData data = new LineData(transform, endPoint, lr, lineObj);

            lineDatas.Add(data);
        }
        private void OnDestroy()
        {
            for (int i = 0; i < lineDatas.Count; i++) lineDatas.Remove(lineDatas[i]);
            foreach (Transform t in transform)
            {
                if (t.TryGetComponent<LineRenderer>(out var l)) DestroyImmediate(l);
            }
        }
        /// <summary>
        /// 指定したラインの位置を起点・終点の現在位置に合わせて更新する。
        /// </summary>
        public void ChangePosition(int index)
        {
            if (index < 0 || index >= lineDatas.Count)
            {
                //Debug.LogError($"ChangePosition: index {index} is out of range. lineDatas.Count: {lineDatas.Count}");
                return;
            }

            if (lineDatas[index].obj.activeSelf && lineDatas[index].start != null && lineDatas[index].end != null)
            {
                lineDatas[index].renderer.SetPosition(0, lineDatas[index].start.position);
                lineDatas[index].renderer.SetPosition(1, lineDatas[index].end.position);
            }
        }

        // ラインの表示切替
        /// <summary>
        /// 指定したラインの表示有無を切り替える。
        /// </summary>
       public void SetLineActive(int index, bool isActive)
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
        

        // すべてのラインをオン
        /// <summary>
        /// すべてのラインの表示有無を切り替える。
        /// </summary>
        public void EnableAllLines(bool eneble)
        {
            foreach (var data in lineDatas)
            {
                data.obj.SetActive(eneble);
            }
        }
    }

}
