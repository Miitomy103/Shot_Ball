using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// 表示文字列の変更処理を持つことを表すインターフェース。
    /// </summary>
    public interface IChangeText
    {
        /// <summary>
        /// 表示する文字列を変更する。
        /// </summary>
        void ChangeText(string str);
    }
}
