using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class SwichBlock : CollisionBase
    {
        bool isEnter = false;

        private Collider2D colli;
        private SpriteRenderer sprite;

        protected override void Start()
        {
            base.Start();
            colli = GetComponent<Collider2D>();
            sprite = GetComponent<SpriteRenderer>();
        }
        protected override void BallEnter(Collision2D collision)
        {
            base.BallEnter(collision);
            isEnter = true;
            colli.isTrigger = true;
            Color color = sprite.color;
            color.a = 0.5f;
            sprite.color = color;
        }
        protected override void StageReset()
        {
            base.StageReset();
            isEnter = false;
            colli.isTrigger = false;
            Color color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }
    }
}
