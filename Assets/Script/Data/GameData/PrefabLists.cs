using UnityEngine;

namespace ShotBall.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PrefabDatas", menuName = "ScriptableObjects/PrefabDatas", order = 1)]
    public class PrefabLists : ScriptableObject
    {
        [SerializeField]public Prefab[] prefabs;
    }
}
