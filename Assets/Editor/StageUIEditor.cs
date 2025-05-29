using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(StageUI))]
    public class StageUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            StageUI stageUI = (StageUI)target;
            base.OnInspectorGUI();
            if(GUILayout.Button("ColorTest"))
            {
                stageUI.TestClearColor();
            }
        }
    }
}
