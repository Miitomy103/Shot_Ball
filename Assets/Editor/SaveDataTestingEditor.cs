using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(SaveDataTesting))]
    public class SaveDataTestingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SaveDataTesting myScript = (SaveDataTesting)target;
            if (GUILayout.Button("All Clear"))
            {
                myScript.AllClear();
            }
        }
    }
}
