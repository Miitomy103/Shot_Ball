using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.Data;

namespace ShotBall
{
    /// <summary>
    /// シーンを跨いでデータを保持するクラス
    /// </summary>
    public class StaticData 
    {
        public static BlockDataWrapper blockDataWrapper;

        public static bool isSelectView = false;
    }
}
