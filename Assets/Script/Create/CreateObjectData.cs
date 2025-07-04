using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    public class CreateObjectData : MonoBehaviour
    {
        [SerializeField] private GimmickDatas[] gimmickDatas; // Changed accessibility to private and serialized field
        public GimmickDatas[] GimmickDatasArray { get; private set; } // Changed to static property for global access
        private void Awake()
        {
            GimmickDatasArray = gimmickDatas;
        }
        [Serializable]
        public class GimmickDatas // Changed accessibility to public  
        {
            public string Name;
            public GimmickData[] dataTypes;
            public string Unique;

            [Serializable]
            public class GimmickData // Changed accessibility to public  
            {
                public string Name;
                public DataType Type;
            }
        }


    }
    public enum DataType
    {
        Int,
        Float,
        Bool,
        Vector2,
        Vectror3,
    }
}
