using ShotBall.Create;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(DefaultData))]
    public class DefaultDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DefaultData defaultData = (DefaultData)target;
            if (GUILayout.Button("ResetData"))
            {
                defaultData.ResetData();
                EditorUtility.SetDirty(defaultData);
            }

        }
    }
}
