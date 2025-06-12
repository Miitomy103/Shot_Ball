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
            if(GUILayout.Button("Load Stage Data"))
            {
                stageData.LoadFile();
                Debug.Log("Stage data loaded successfully.");
            }
            if (GUILayout.Button("Clear Stage Data"))
            {
                stageData.DelateObject();
                Debug.Log("Stage data cleared.");
            }
        }
    }
}
