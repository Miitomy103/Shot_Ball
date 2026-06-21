using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージのマス情報を持つことを表すインターフェース。
    /// </summary>
    public interface IGridData
    {
        int[,] GridData { get; }
        bool Center { get; }
    }
}
