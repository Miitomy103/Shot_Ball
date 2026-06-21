using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ブロックの種別データを持つことを表すインターフェース。
    /// </summary>
    public interface IBlockData
    {
        /// <summary>
        /// このブロックの種別。
        /// </summary>
        public BlockType Type { get; }
    }
}
