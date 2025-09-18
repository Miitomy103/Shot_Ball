using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(SaveDataTeatiing))]
    public class SaveDataTeatiingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SaveDataTeatiing myScript = (SaveDataTeatiing)target;
            if (GUILayout.Button("All Clear"))
            {
                myScript.AllClear();
            }
        }
    }
}
