using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class PrefabSpawnerRuntime : PrefabSpawnerComponent
    {
        protected override BehaviourType behaviourType => BehaviourType.Runtime;
    }
}
