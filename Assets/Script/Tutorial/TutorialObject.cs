using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{

    public class TutorialObject : MonoBehaviour
    {
        private RuntimeDragObject dragObject;
        private RotationDragObject rotationObject;

        [SerializeField] private TutorialManager tutorialManager;

        private void Awake()
        {
            dragObject = GetComponent<RuntimeDragObject>();
            rotationObject = GetComponent<RotationDragObject>();
        }

        private void Start()
        {
            if (dragObject != null)
            {
                dragObject.DragEndAction += OnDragEnd;
            }

            if (rotationObject != null)
            {
                rotationObject.Action += OnRotation;
            }
        }

        private void OnDestroy()
        {
            if (dragObject != null)
            {
                dragObject.DragEndAction -= OnDragEnd;
            }

            if (rotationObject != null)
            {
                rotationObject.Action -= OnRotation;
            }
        }

        private void OnDragEnd()
        {
            // Frame外に置いたら Move チュートリアル達成
            if (!dragObject.InFrame)
            {
                tutorialManager?.ClearMove();
            }
        }

        private void OnRotation()
        {
            float zAngle = NormalizeAngle(transform.eulerAngles.z);

            // 135°以上回したらクリア判定
            if ((Mathf.Abs(zAngle) >= 135f&& Mathf.Abs(zAngle) <= 157.5f)||( Mathf.Abs(zAngle) >= -45f && Mathf.Abs(zAngle) <= -22.5f))
            {
                tutorialManager?.ClearRotation();
            }
        }

        /// <summary>
        /// 角度を -180°～180° の範囲に正規化
        /// </summary>
        private float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle > 180f) angle -= 360f;
            return angle;
        }
    }

}
