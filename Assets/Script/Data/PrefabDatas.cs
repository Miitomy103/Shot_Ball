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
        private void OnValidate()
        {
            for(int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i].IsAttached==false)
                {
                    if (prefabs[i].PrefabObj.TryGetComponent<GimmickBase>(out var component))
                    {
                        prefabs[i].IsAttached = true;
                        prefabs[i].name = component.Name;
                    }
                }
            }
        }
    }
}
