using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// リサイズ中かどうかを持つことを表すインターフェース。
    /// </summary>
    public interface IResize
    {
        /// <summary>
        /// 現在リサイズ中かどうか。
        /// </summary>
        public bool IsResize { get;}
    }
}
