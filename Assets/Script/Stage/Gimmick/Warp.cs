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

        [SerializeField] AudioSource warpSound;

        private void OnValidate()
        {
            if(!IsWarpChildren()) warpChildren = GetComponentsInChildren<WarpChild>();

            foreach(var w in warpChildren)
            {
                w.ColorChange(color);
            }
        }
        protected override void Awake()
        {
            base.Awake();
            WarpManager.Initialization();
        }
        protected override void Start()
        {
            base.Start();
            WarpManager.AddWarp(this);
        }
        protected override void StageReset()
        {
            base.StageReset();
            didWarp = false;
        }
        public void ChildGenerate(WarpChild warpChild)
        {
            for(int i=0;i<warpChildren.Length;i++)
            {
                if (warpChildren[i] == null)
                {
                    warpChildren[i] = warpChild;
                    break;
                }
            }
            foreach (var w in warpChildren)
            {
                if(w!=null) w.ColorChange(color);
            }
        }
        public void ChildDestroy(WarpChild warpChild)
        {
            for (int i = 0; i < warpChildren.Length; i++)
            {
                if (warpChildren[i] == warpChild) warpChildren[i] = null;
            }
            lineRenderer.enabled = false;
        }
        public bool Available()
        {
            foreach (var w in warpChildren) if (w == null) return true;
            return false;
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
        public void OnBallInWarp(Collider2D other,WarpChild warpChild)
        {
            if (!IsLine()) return;
            if (didWarp) return;
            for (int i = 0; i < warpChildren.Length; i++)
            {
                if (warpChildren[i]!=warpChild && warpChildren[i]!=null)
                {
                    didWarp = true;
                    other.transform.position = warpChildren[i].transform.position;
                    warpSound.Play();
                }
            }
        }

        protected override bool IsLine()
        {
            return !IsChildrenNull();
        }
        bool IsChildrenNull()
        {
            foreach(var w in warpChildren)
            {
                if (w == null) return true;
            }
            return false;
        }
    }
}
