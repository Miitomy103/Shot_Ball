using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class TutorialObject : MonoBehaviour
    {
        RuntimeDragObject dragObject;
        RotationDragObject rotationObject;

        [SerializeField] TutorialManager tutorialManager;
        private void Awake()
        {
            dragObject = GetComponent<RuntimeDragObject>();
            rotationObject = GetComponent<RotationDragObject>();
        }
        private void Start()
        {
            dragObject.DragStartAction += DragStart;
            dragObject.DragEndAction += DragEnd;
            rotationObject.Action += Rotation;
        }
        void DragStart()
        {

        }
        void DragEnd()
        {
            if (!dragObject.InFrame)
            {
                tutorialManager.MoveClear();
            }
        }
        void Rotation()
        {
            float zAngle = transform.eulerAngles.z;
            if (zAngle>= 135|| zAngle < -180f)
            {
                tutorialManager.RotationClear();
            }

        }
    }
}
