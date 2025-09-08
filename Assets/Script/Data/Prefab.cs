using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Data
{
    [System.Serializable]
    public class Prefab 
    {
        [SerializeField] public BlockType type; // Prefab‚Ìí—Ş
        [SerializeField] GameObject prefab;
        public GameObject PrefabObj => prefab;
    }
}
