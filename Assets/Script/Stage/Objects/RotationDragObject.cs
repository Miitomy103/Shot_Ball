using ShotBall.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class RotationDragObject : MonoBehaviour
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
            if (GameLoop.StageState != StageState.Setting||dragObject==null) return;
            StartCoroutine(RotateUntilPlaced());
        }

        IEnumerator RotateUntilPlaced()
        {
            Rotate.Instance.Play();

            float totalRotation = 0;

            while (totalRotation < 360f)
            {
                ObjectRotation();
                totalRotation += moveRotation;
                Debug.Log("Rotation");

                // 1ƒtƒŒ[ƒ€‘Ò‚Â
                yield return new WaitForFixedUpdate();

                if (dragObject.IsPlaced())
                {
                    Debug.Log("PlacedTrue");
                    break;
                }

                Debug.Log("PlacedFalse");
            }

            Action?.Invoke();
        }


        void ObjectRotation()
        {
            transform.Rotate(0, 0, moveRotation);
        }
    }

}
