using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class RotationDragObject:MonoBehaviour
    {
        [SerializeField] float moveRotation = 45;
        DragObject dragObject;

        public Action Action { get; set; }
        private void Awake()
        {
            dragObject = GetComponent<DragObject>();
        }
        public void IsClick()
        {
            if (GameLoop.StageState != StageState.Setting) return;
            int count = 0;
            while(count<10)
            {
                ObjectRotation();
                count++;
                Debug.Log("Rotation");
                if (dragObject.IsPlaced()) break;
            }
            Action();
        }
        void ObjectRotation()
        {
            transform.Rotate(0, 0, moveRotation);
        }
    }
}
