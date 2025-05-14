using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DragObjectSelector : MonoBehaviour
    {
        public BehaviourType behaviourType;

        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            Debug.Log("You have not selected a BahaviourType");
            Debug.Break();
        }
        private void OnValidate()
        {
            EditorApplication.delayCall += () =>
            {

                switch (behaviourType)
                {
                    case BehaviourType.Runtime:
                        if (!GetComponent<RuntimeDragObject>())
                            AddBehaviour<RuntimeDragObject>();
                        OrderChange(7);
                        break;
                    case BehaviourType.Editor:
                        if (!GetComponent<EditorDragObject>())
                            AddBehaviour<EditorDragObject>();
                        OrderChange(0);
                        break;
                }
            };
        }
        private void OrderChange(int layer)
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sortingOrder = layer;
        }
        private void AddBehaviour<T>() where T:DragObject
        {
            RemoveDragObject();
            gameObject.AddComponent<T>();
            DestroyImmediate(this);
        }
        private void RemoveDragObject()
        {
            // DragObject Ç‹ÇΩÇÕÇªÇÃîhê∂ÇÇ∑Ç◊ÇƒéÊìæ
            var dragObjects = GetComponents<DragObject>();
            foreach (var obj in dragObjects)
            {
                if (obj != this)  // é©ï™é©êgÇÕè¡Ç≥Ç»Ç¢ÇÊÇ§Ç…ï€åÏ
                    DestroyImmediate(obj);
            }
        }

    }
    public enum BehaviourType
    {
        None,
        Runtime,
        Editor,
    }

}
