using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class CollisionBase : GimmickBase
    {
        [SerializeField] protected LayerMask ballLayer;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == ballLayer || GameLoop.StageState != StageState.Playing||!IsCollision()) return;
            BallEnter(collision);
        }
        protected virtual void BallEnter(Collision2D collision) { }
        protected virtual void BallExit(Collision2D collision) { }
        protected virtual bool IsCollision() { return true; }
    }
}
