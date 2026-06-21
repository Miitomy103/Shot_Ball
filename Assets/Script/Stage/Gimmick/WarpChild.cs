using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// Warpの片側の出入口。配置時に親Warp(エディット用は親オブジェクト、プレイ用はWarpManagerから空き枠)に登録される。
    /// </summary>
    public class WarpChild : AreaBase
    {
        public Warp parentWarp { get; private set; }

        private SpriteRenderer sprite;


        protected override void Start()
        {
            base.Start();
            if(GetComponent<DragObject>() == null)
            {
                parentWarp=transform.parent.GetComponent<Warp>();
            }
            else
            {
                parentWarp = WarpManager.WarpAvailable();
            }
            if (parentWarp == null) return;
            parentWarp.ChildGenerate(this);
        }
        private void OnDestroy()
        {
            if (parentWarp == null) return;
            parentWarp.ChildDestroy(this);
        }
        /// <summary>
        /// スプライトの色を変更する。
        /// </summary>
        public void ColorChange(Color color)
        {
            if (sprite == null) sprite = GetComponent<SpriteRenderer>();

            sprite.color = color;
        }
        protected override void BallEnter(Ball other)
        {
            parentWarp.OnBallInWarp(other,this);
        }

        public override void LoadData(StageBlockData data)
        {

        }
    }
}
