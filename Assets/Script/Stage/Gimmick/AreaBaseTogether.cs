using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ボールが入った/出たどちらの場合でも同じ判定処理を呼び出すAreaBase。
    /// </summary>
    public abstract class AreaBaseTogether : AreaBase
    {
        protected override void BallEnter(Ball other)
        {
            BallCheck(other);
        }

        protected override void BallExit(Ball other)
        {
            BallCheck(other);
        }

        /// <summary>
        /// ボールが入った/出たときに呼ばれる判定処理。
        /// </summary>
        protected abstract void BallCheck(Ball other);
    }
}
