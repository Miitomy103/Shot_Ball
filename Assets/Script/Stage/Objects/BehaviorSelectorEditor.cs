using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(DragObjectSelector))]
    public class BehaviorSelectorEditor : Editor
    {
        //private void OnValidate()
        //{
        //    DragObjectSelector selector = (DragObjectSelector)target;
        //    AddBehaviorComponent(selector);
        //}
        //public override void OnInspectorGUI()
        //{
        //    base.OnInspectorGUI();

        //    DragObjectSelector selector = (DragObjectSelector)target;

        //    if (GUILayout.Button("Apply Behavior"))
        //    {
        //        AddBehaviorComponent(selector);
        //    }
        //}

        private void AddBehaviorComponent(DragObjectSelector selector)
        {
            // 既存のBehaviorスクリプトを削除（任意）
            var existing = selector.GetComponent<MonoBehaviour>();
            if (existing && existing != selector)
            {
                DestroyImmediate(existing);
            }

            // enumに応じて追加
            switch (selector.behaviourType)
            {
                case BehaviourType.Runtime:
                    if (!selector.GetComponent<RuntimeDragObject>())
                        selector.gameObject.AddComponent<RuntimeDragObject>();
                    break;
                case BehaviourType.Editor:
                    if (!selector.GetComponent<EditorDragObject>())
                        selector.gameObject.AddComponent<EditorDragObject>();
                    break;
            }
        }
    }
}
