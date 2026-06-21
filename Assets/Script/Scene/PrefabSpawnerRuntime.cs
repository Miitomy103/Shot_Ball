using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 実行時(プレイ中)の挙動でプレハブを生成するスポナー。
    /// </summary>
    public class PrefabSpawnerRuntime : PrefabSpawnerComponent
    {
        protected override BehaviourType behaviourType => BehaviourType.Runtime;
    }
}
