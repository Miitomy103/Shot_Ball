using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
{
    public class WarpChild : AreaBase
    {
        public Warp parentWarp { get; private set; }
        public int elements { get; private set; }

        private SpriteRenderer sprite;

        public bool InFrame
        {
            get
            {
                if (dragObject != null) return dragObject.inFrame;
                return false;
            }
        }
        RuntimeDragObject dragObject;
        protected override void Awake()
        {
            base.Awake();
            dragObject = GetComponent<RuntimeDragObject>();
        }
        public void ColorChange(Color color)
        {
            if (sprite == null) sprite = GetComponent<SpriteRenderer>();

            sprite.color = color;
        }

        public void Initialize(Warp parent,int element)
        {
            parentWarp = parent;
            elements = element;
        }
        protected override void BallEnter(Collider2D other)
        {
            parentWarp.OnBallInWarp(other,elements);
        }
    }
}
