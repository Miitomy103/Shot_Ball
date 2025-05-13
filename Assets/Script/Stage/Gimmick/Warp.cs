using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Warp : GimmickBase
    {
        [SerializeField] WarpChild[] warpChildren;

        bool didWarp = false;
        [SerializeField] Color color;

        private LineRenderer lineRenderer;
        private void OnValidate()
        {
            if(!IsWarpChildren()) warpChildren = GetComponentsInChildren<WarpChild>();

            foreach(var w in warpChildren)
            {
                w.ColorChange(color);
            }
            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, warpChildren[0].transform.position);
            lineRenderer.SetPosition(1, warpChildren[1].transform.position);
        }
        protected override void Start()
        {
            base.Start();
            if (!IsWarpChildren()) warpChildren = GetComponentsInChildren<WarpChild>();
            if(lineRenderer==null)lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;

            if (!IsWarpChildren())Debug.LogError("NullWarps");

            for(int i=0;i<warpChildren.Length;i++)
            {
                warpChildren[i].Initialize(this, i);
            }
        }
        private void FixedUpdate()
        {
            if (GameLoop.StageState != StageState.Setting) return;
            lineRenderer.SetPosition(0, warpChildren[0].transform.position);
            lineRenderer.SetPosition(1, warpChildren[1].transform.position);
        }
        bool IsWarpChildren()
        {
            foreach(var child in warpChildren)
            {
                if (child == null) return false;
            }
            if (warpChildren.Length != 2 )
            {
                return false;
            }
            return true;
        }
        public void OnBallInWarp(Collider2D other,int elements)
        {
            if (didWarp) return;
            for (int i = 0; i < warpChildren.Length; i++)
            {
                if(i!=elements)
                {
                    didWarp = true;
                    other.transform.position = warpChildren[i].transform.position;
                }
            }
        }
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
