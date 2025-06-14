using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.InGame;

namespace ShotBall.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PrefabDatas", menuName = "ScriptableObjects/PrefabDatas", order = 1)]
    public class PrefabDatas : ScriptableObject
    {
        [SerializeField]public Prefab[] prefabs;
    }
}
