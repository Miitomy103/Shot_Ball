using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 複数のTargets間をLineRendererで結んで表示するギミックの基底クラス。
    /// </summary>
    public abstract class LineBase : GimmickBase
    {
        protected abstract Color Color { get; }

        protected LineRenderer lineRenderer;
        /// <summary>
        /// ラインで結ぶ対象の位置一覧。
        /// </summary>
        protected abstract Transform[] Targets { get; }
        protected abstract float startWidth { get; }
        protected abstract float endWidth { get; }
        private void OnValidate()
        {
            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }

            lineRenderer.startColor = Color;
            lineRenderer.endColor = Color;

            lineRenderer.positionCount = Targets.Length;
            for(int i=0;i<Targets.Length;i++)
            {
                lineRenderer.SetPosition(i, Targets[i].position);
            }
        }
        protected override void Start()
        {
            base.Start();

            if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.positionCount = Targets.Length;
            lineRenderer.startWidth = startWidth;
            lineRenderer.endWidth = endWidth;
        }
        private void FixedUpdate()
        {
            if (!IsLine())
            {
                lineRenderer.enabled = false;
                return;
            }
            lineRenderer.enabled = true;
            for (int i = 0; i < Targets.Length; i++)
            {
                lineRenderer.SetPosition(i, Targets[i].position);
            }
        }
        /// <summary>
        /// ラインを表示するべき状態かどうか。
        /// </summary>
        protected abstract bool IsLine();
        protected override void StageStart()
        {
            lineRenderer.enabled = false;
        }
        protected override void StageReset()
        {
            lineRenderer.enabled = true;
        }
    }
}
