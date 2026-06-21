using UnityEngine;

namespace ShotBall.Data
{
    /// <summary>
    /// ブロック種別とそれに対応するプレハブを結び付けるデータクラス。
    /// </summary>
    [System.Serializable]
    public class Prefab
    {
        [SerializeField] public BlockType type; // Prefabの種類
        [SerializeField] GameObject prefab;
        public GameObject PrefabObj => prefab;
    }
}
