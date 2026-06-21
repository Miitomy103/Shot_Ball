using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// インスペクターで設定したギミックデータの配列を保持するクラス。
    /// </summary>
    public class CreateObjectData : MonoBehaviour
    {
        [SerializeField] private GimmickDatas[] gimmickDatas; // Changed accessibility to private and serialized field
        /// <summary>
        /// 設定されたギミックデータの配列。
        /// </summary>
        public GimmickDatas[] GimmickDatasArray { get; private set; } // Changed to static property for global access
        private void Awake()
        {
            GimmickDatasArray = gimmickDatas;
        }
        /// <summary>
        /// ギミックの名前とユニークIDを保持するデータクラス。
        /// </summary>
        [Serializable]
        public class GimmickDatas // Changed accessibility to public
        {
            public BlockType Name;
            public string Unique;
        }


    }
    /// <summary>
    /// パラメータの型を表す列挙型。
    /// </summary>
    public enum DataType
    {
        Int,
        Float,
        Bool,
        Vector2,
        Vectror3,
        Arrow,
    }
}
