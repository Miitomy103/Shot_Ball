using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// StageBlockDataを持つことを表すインターフェース。
    /// </summary>
    public interface IStageBlockData
    {
        /// <summary>
        /// このオブジェクトが持つステージブロックデータ。
        /// </summary>
        StageBlockData StageBlockData { get; }
    }
}
