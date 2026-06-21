using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// エディター編集中の挙動でプレハブを生成するスポナー。
    /// </summary>
    public class PrefabSpawnerEditor : PrefabSpawnerComponent
    {
        protected override BehaviourType behaviourType => BehaviourType.Editor;
    }
}
