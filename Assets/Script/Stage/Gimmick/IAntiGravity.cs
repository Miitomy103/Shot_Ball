using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 無重力状態を切り替えられることを表すインターフェース。
    /// </summary>
    public interface IAntiGravity
    {
        /// <summary>
        /// 無重力状態を反転させる。
        /// </summary>
        public void ChangeGravity();

        /// <summary>
        /// 現在無重力状態かどうか。
        /// </summary>
        public bool AntiGravity { get; }

    }
}
