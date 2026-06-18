using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// Parameter‚ðSO‚Å‚Ü‚Æ‚Ü‚é
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Block Definitions")]
    public  class Parameters : ScriptableObject
    {
        public BlockDefinitionSO[] blockDefinitions;
    }
}
