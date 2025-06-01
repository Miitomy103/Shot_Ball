using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Warp : LineBase
    {
        [SerializeField] WarpChild[] warpChildren;

        bool didWarp = false;
        protected override Transform[] Targets => warpChildren.Select(w => w.transform).ToArray();

        [SerializeField] Color color;
        protected override Color Color => color;

        protected override float startWidth => 0.05f;
        protected override float endWidth => 0.05f;

        public override string Name => "Warp";

        private void OnValidate()
        {
            if(!IsWarpChildren()) warpChildren = GetComponentsInChildren<WarpChild>();

            foreach(var w in warpChildren)
            {
                w.ColorChange(color);
            }
        }
        protected override void Start()
        {
            base.Start();
            if (!IsWarpChildren()) warpChildren = GetComponentsInChildren<WarpChild>();

            if (!IsWarpChildren())Debug.LogError("NullWarps");

            for(int i=0;i<warpChildren.Length;i++)
            {
                warpChildren[i].Initialize(this, i);
            }
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
            if (!IsLine()) return;
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

        protected override bool IsLine()
        {
            foreach (var w in warpChildren)
            {
                if (w.InFrame) return false;
            }
            return true;
        }
    }
}
