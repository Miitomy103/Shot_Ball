using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public static class WarpManager
    {
        public static List<Warp> warpList = new List<Warp>();
        public static void AddWarp(Warp warp)
        {
            warpList.Add(warp);
        }
        public static Warp WarpAvailable()
        {
            foreach (var w in warpList) if (w.Available()) return w;
            return null;
        }
        public static void Initialization()
        {
            warpList = new List<Warp>();
        }
    }
}
