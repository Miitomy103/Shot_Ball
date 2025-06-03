using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class AreaBase : GimmickBase
    {
        [SerializeField]protected LayerMask ballLayer;
        protected override void Start()
        {
            base.Start();
            OrderChange();
        }
        protected virtual void OrderChange()
        {
            ThisSprite.sortingOrder = (int)OrderInLayer.Area;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer||GameLoop.StageState!=StageState.Playing) return;
            BallEnter(other);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer || GameLoop.StageState != StageState.Playing) return;
            BallExit(other);
        }
        protected virtual void BallEnter(Collider2D other) { }
        protected virtual void BallExit(Collider2D other) { }
    }
}
