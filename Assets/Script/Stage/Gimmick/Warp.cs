using System.Linq;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 2つのWarpChild間をボールが通過するとワープさせるギミック。
    /// </summary>
    public class Warp : LineBase
    {
        [SerializeField] WarpChild[] warpChildren;

        bool didWarp = false;
        protected override Transform[] Targets => warpChildren.Select(w => w.transform).ToArray();

        [SerializeField] Color color;
        protected override Color Color => color;

        protected override float startWidth => 0.05f;
        protected override float endWidth => 0.05f;

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
        /// <summary>
        /// 空いている枠にWarpChildを登録する。
        /// </summary>
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
        /// <summary>
        /// 登録済みのWarpChildを枠から外す。
        /// </summary>
        public void ChildDestroy(WarpChild warpChild)
        {
            for (int i = 0; i < warpChildren.Length; i++)
            {
                if (warpChildren[i] == warpChild) warpChildren[i] = null;
            }
            lineRenderer.enabled = false;
        }
        /// <summary>
        /// WarpChildを追加できる空き枠があるかどうか。
        /// </summary>
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
        /// <summary>
        /// ボールがこのワープのWarpChildに入ったときに、もう一方のWarpChildへ移動させる。
        /// </summary>
        public void OnBallInWarp(Ball other,WarpChild warpChild)
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

        public override void LoadData(StageBlockData data)
        {

        }
    }
}
