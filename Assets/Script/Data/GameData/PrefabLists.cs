using UnityEngine;

namespace ShotBall.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PrefabDatas", menuName = "ScriptableObjects/PrefabDatas", order = 1)]
    /// <summary>
    /// ブロック種別とプレハブの対応リストを保持するScriptableObject。
    /// </summary>
    public class PrefabLists : ScriptableObject
    {
        [SerializeField]public Prefab[] prefabs;
    }
}
