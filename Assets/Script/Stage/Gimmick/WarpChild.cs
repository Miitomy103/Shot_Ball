using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
{
    public class WarpChild : AreaBase
    {
        public Warp parentWarp { get; private set; }

        private SpriteRenderer sprite;


        public override string Name => "WarpChild";

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
        public void ColorChange(Color color)
        {
            if (sprite == null) sprite = GetComponent<SpriteRenderer>();

            sprite.color = color;
        }
        protected override void BallEnter(Collider2D other)
        {
            parentWarp.OnBallInWarp(other,this);
        }
    }
}
