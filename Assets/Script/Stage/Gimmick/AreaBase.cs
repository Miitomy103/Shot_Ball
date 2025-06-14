using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class AreaBase : GimmickBase
    {
        [SerializeField]protected LayerMask ballLayer;

        List<Ball> enterBalls = new List<Ball>();
        List<Ball> exitBalls = new List<Ball>();

        protected override void Start()
        {
            base.Start();
            OrderChange();
        }
        protected virtual void OrderChange()
        {
            ThisSprite.sortingOrder = (int)OrderInLayer.Area;
        }
        override protected void StageStart()
        {
            base.StageStart();
            enterBalls.Clear();
            exitBalls.Clear();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer||GameLoop.StageState!=StageState.Playing) return;
            if (other.gameObject.TryGetComponent<Ball>(out var ball))
            {
                foreach (var b in enterBalls)
                {
                    if (b == ball) return;
                }
                BallExit(ball);
                enterBalls.Add(other.GetComponent<Ball>());
            }
            else
            {
                Debug.LogWarning("AreaBaseに衝突したオブジェクトがBallではありません: " + other.gameObject.name);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer || GameLoop.StageState != StageState.Playing) return;
            if(other.gameObject.TryGetComponent<Ball>(out var ball))
            {
                foreach (var b in exitBalls)
                {
                    if (b == ball) break;
                }
                BallExit(ball);
                exitBalls.Add(other.GetComponent<Ball>());
            }
        }
        protected virtual void BallEnter(Ball other) { }
        protected virtual void BallExit(Ball other) { }
    }
}
