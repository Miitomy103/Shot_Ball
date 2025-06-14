using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(FramesManager))]
    public class FramesManagerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            FramesManager framesManager = (FramesManager)target;
            if (GUILayout.Button("Generate Frames"))
            {
                //framesManager.GameStart();
            }
        }
    }
}
