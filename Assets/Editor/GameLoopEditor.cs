using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    [CustomEditor(typeof(GameLoop))]
    public class GameLoopEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GameLoop gameLoop = (GameLoop)target;
            base.OnInspectorGUI();
            if(GUILayout.Button("GameReset"))
            {
                gameLoop.GameReset();
            }

        }
    }
}
