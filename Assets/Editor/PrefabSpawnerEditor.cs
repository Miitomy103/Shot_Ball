using UnityEditor;
using UnityEngine;
using ShotBall.InGame;
using Unity.VisualScripting;

[CustomEditor(typeof(PrefabSpawnerComponent), true)]
[CanEditMultipleObjects]
public class PrefabSpawnerComponentEditor : Editor
{
    GameObject[] objects;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PrefabSpawnerComponent prefabs = (PrefabSpawnerComponent)target;

        if (prefabs == null) return;
        if (prefabs.DragObjects == null || prefabs.DragObjects.prefabs == null) return;

        objects = prefabs.DragObjects.prefabs;

        for (int i = 0; i < objects.Length; i++)
        {
            if (GUILayout.Button($"{objects[i].name}"))
            {
                // 普通にキャストして呼ぶ（dynamicは使わない）
                prefabs.PrefabInstance(i);
            }
        }
    }

}

