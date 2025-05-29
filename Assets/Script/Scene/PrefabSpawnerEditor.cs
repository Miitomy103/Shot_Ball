using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class PrefabSpawnerEditor : PrefabSpawnerComponent
    {
        protected override BehaviourType behaviourType => BehaviourType.Editor;
    }
}
