using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージの進行状態を表す列挙型。
    /// </summary>
    public enum StageState
    {
        Setting,
        Playing,
        Pose,
        Clear,
    }
}
