using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.Data;

namespace ShotBall
{
    /// <summary>
    /// シーン読み込み時に受け渡すブロックデータを保持する静的クラス。
    /// </summary>
    public static class LoadScene
    {
        /// <summary>
        /// 読み込み先シーンに渡すブロックデータ。
        /// </summary>
        public static BlockDataWrapper BlockDataWrapper;
    }
}
