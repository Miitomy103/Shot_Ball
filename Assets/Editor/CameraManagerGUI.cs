using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(CameraManager),true)]
    public class CameraManagerGUI : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CameraManager camera = target as CameraManager;
            if (GUILayout.Button("AdjustWalls"))
            {
                camera.OnGUIButton();
            }
            if (GUILayout.Button("Culcuration"))
            {
                camera.Culcuration();
            }
        }
    }
}
