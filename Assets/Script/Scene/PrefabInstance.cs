using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class PrefabSpawnerComponent: MonoBehaviour
    {
        protected abstract BehaviourType behaviourType { get; }

        [SerializeField]public DragObjectsEditor DragObjects;
        /// <summary>
        /// インデックスを受け取って Prefab をインスタンス化する例
        /// </summary>
        public virtual void PrefabInstance(int index)
        {
            if (index < 0 || index >= DragObjects.prefabs.Length) return;
            GameObject obj= Instantiate(DragObjects.prefabs[index], transform.position, Quaternion.identity,transform);

            DragObjectSelector objectSelector = obj.GetComponent<DragObjectSelector>();
            if (objectSelector == null) return;
            objectSelector.TypeChange(behaviourType);
        }
    }

}
