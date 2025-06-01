using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class RotationDragObject:MonoBehaviour
    {
        [SerializeField] float moveRotation = 45;
        DragObject dragObject;

        private void Awake()
        {
            dragObject = GetComponent<DragObject>();
        }
        public void IsClick()
        {
            int count = 0;
            while(count<10)
            {
                ObjectRotation();
                count++;
                Debug.Log("Rotation");
                if (dragObject.IsPlaced()) break;
            }
        }
        void ObjectRotation()
        {
            transform.Rotate(0, 0, moveRotation);
        }
    }
}
