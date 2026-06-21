using ShotBall.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// クリックすると、配置できるまで(またはAngle分)同じオブジェクトを回転させ続けるクラス。
    /// </summary>
    public class RotationDragObject : MonoBehaviour
    {
        [SerializeField] float moveRotation = 45;
        DragObject dragObject;

        public Action Action { get; set; }

        private void Awake()
        {
            dragObject = GetComponent<DragObject>();
        }

        /// <summary>
        /// クリックされたときに回転を開始する(設定中のみ)。
        /// </summary>
        public void IsClick()
        {
            if (GameLoop.StageState != StageState.Setting||dragObject==null) return;
            StartCoroutine(RotateUntilPlaced());
        }

        /// <summary>
        /// 配置可能になるまで、またはちょうど一周するまで回転を続ける。
        /// </summary>
        IEnumerator RotateUntilPlaced()
        {
            Rotate.Instance.Play();

            float totalRotation = 0;

            while (totalRotation < 360f)
            {
                ObjectRotation();
                totalRotation += moveRotation;
                Debug.Log("Rotation");

                // 1フレーム待つ
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
