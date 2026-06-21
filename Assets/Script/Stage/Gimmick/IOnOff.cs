using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// オン/オフの状態を切り替えられることを表すインターフェース。
    /// </summary>
    public interface IOnOff
    {
        /// <summary>
        /// 現在オン状態かどうか。
        /// </summary>
        bool IsOn { get;}
        /// <summary>
        /// オン状態にする。
        /// </summary>
        void ItOn();
        /// <summary>
        /// オフ状態にする。
        /// </summary>
        void ItOff();
        //TODO:StringLoadできるようにする
    }
}
