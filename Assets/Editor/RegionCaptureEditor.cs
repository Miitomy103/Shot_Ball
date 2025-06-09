using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(RegionCapture))]


    public class RegionCaptureEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            RegionCapture regionCapture = (RegionCapture)target;
            if (GUILayout.Button("Capture Region"))
            {
                regionCapture.Capture();
            }
        }
    }
}
