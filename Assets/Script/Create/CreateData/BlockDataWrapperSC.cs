using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Data
{
    /// <summary>
    /// BlockDataWrapperをアセット化するためのScriptableObject。
    /// </summary>
    [CreateAssetMenu(menuName = "MyGame/BlockDataWrapper")]
    public class BlockDataWrapperSC : ScriptableObject
    {
        public BlockDataWrapper BlockDataWrapper;
    }
}
