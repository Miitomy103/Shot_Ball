using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージ内の全Warpを管理する静的クラス。WarpChildが接続先を探すのに使う。
    /// </summary>
    public static class WarpManager
    {
        public static List<Warp> warpList = new List<Warp>();
        /// <summary>
        /// Warpを管理対象に追加する。
        /// </summary>
        public static void AddWarp(Warp warp)
        {
            warpList.Add(warp);
        }
        /// <summary>
        /// 空き枠があるWarpを探して返す。
        /// </summary>
        public static Warp WarpAvailable()
        {
            foreach (var w in warpList) if (w.Available()) return w;
            return null;
        }
        /// <summary>
        /// 管理リストを初期化する。
        /// </summary>
        public static void Initialization()
        {
            warpList = new List<Warp>();
        }
    }
}
