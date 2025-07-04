using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Block Definitions")]
    public  class Parametors : ScriptableObject
    {
        public BlockDefinition[] blockDefinitions;
    }
}
