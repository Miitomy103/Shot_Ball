using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ShotBall.Data
{
    [CustomEditor(typeof(StageData))]
    public class StageDataEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            StageData stageData = (StageData)target;
            if (GUILayout.Button("Save Stage Data"))
            {
                stageData.Save();
                Debug.Log("Stage data saved successfully.");
            }
        }
    }
}
