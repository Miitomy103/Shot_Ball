using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ボールと衝突するギミックの基底クラス
    /// </summary>
    public abstract class CollisionBase : GimmickBase
    {
        [SerializeField] protected LayerMask ballLayer;
        protected Collider2D Collider2D { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            Collider2D = GetComponent<Collider2D>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == ballLayer || GameLoop.StageState != StageState.Playing||!IsCollision()) return;
            BallEnter(collision);
        }
        /// <summary>
        /// ボールと衝突したときの処理を記述するメソッド
        /// </summary>
        protected virtual void BallEnter(Collision2D collision) { }
        /// <summary>
        /// ボールと衝突している間の処理を記述するメソッド
        /// </summary>
        protected virtual void BallExit(Collision2D collision) { }

        /// <summary>
        /// 衝突処理を行うかどうかを返すメソッド
        /// </summary>
        protected virtual bool IsCollision() { return true; }
    }
}
