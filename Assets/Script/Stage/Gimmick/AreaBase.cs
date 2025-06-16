using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class AreaBase : GimmickBase
    {
        [SerializeField]protected LayerMask ballLayer;

        private readonly List<Ball> enterBalls = new List<Ball>();
        private readonly List<Ball> exitBalls = new List<Ball>();

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
            if (other.gameObject.layer == ballLayer || GameLoop.StageState != StageState.Playing) return;

            if (other.TryGetComponent<Ball>(out var ball))
            {
                if (!enterBalls.Contains(ball))
                {
                    enterBalls.Add(ball);
                    BallEnter(ball); // ← 本来ここで呼ぶべき
                }
            }
            else
            {
                Debug.LogWarning("AreaBaseに衝突したオブジェクトがBallではありません: " + other.gameObject.name);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer || GameLoop.StageState != StageState.Playing) return;

            if (other.TryGetComponent<Ball>(out var ball))
            {
                if (enterBalls.Contains(ball))
                {
                    enterBalls.Remove(ball);
                    BallExit(ball); // ← 本来ここで呼ぶべき
                }
            }
        }

        protected virtual void BallEnter(Ball other) { }
        protected virtual void BallExit(Ball other) { }
    }
}
