using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Data
{
    [System.Serializable]
    public class Prefab 
    {
        [SerializeField]public string name;
        [SerializeField] GameObject prefab;
        public GameObject PrefabObj => prefab;
        public bool IsAttached { get; set; }
    }
}
