using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class AutoRotator : GimmickBase,IOnOff
    {
        public override string Name => "AutoRotator";

        bool isOn;
        public bool IsOn => isOn;

        public Transform Transform => transform;

        [SerializeField] Vector3 s_RotationSpeed = new Vector3(0, 0, 45);
        Vector3 rotationSpeed;

        Quaternion startRotation = new Quaternion();

        bool isStop;

        Collider2D[] childCollis;
        ObjectBase dragObject;
        protected override void Awake()
        {
            base.Awake();
            childCollis = GetComponentsInChildren<Collider2D>();
            dragObject = GetComponent<ObjectBase>();
            rotationSpeed = s_RotationSpeed;
        }
        protected override void Start()
        {
            base.Start();
            startRotation = transform.rotation;
        }
        
        protected override void StageReset()
        {
            base.StageReset();
            transform.rotation = startRotation;
        }
        private void Update()
        {
            Rotate();
            if (GameLoop.StageState != StageState.Setting) return;
            MouseInputHandler inputHandler = MouseInputHandler.Instance;
            if(inputHandler.LeftButton && inputHandler.IsMove)
            {
                isStop = false;
            }
            if(inputHandler.LeftUp)
            {
                isStop = false;
            }
        }
        void Rotate()
        {
            if (isStop) return;

            if (dragObject != null && dragObject.PublicOverLapping())
            {
                isStop = true;
                return;
            }
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
        protected override void Drag()
        {
            base.Drag();
            isStop = false;
        }
        protected override string StringData()
        {
            return $"{s_RotationSpeed.x},{s_RotationSpeed.y},{s_RotationSpeed.z}";
        }
        public override void LoadData(string data)
        {
            base.LoadData(data);
            string[] splitData = data.Split(',');
            s_RotationSpeed.x = float.Parse(splitData[0]);
            s_RotationSpeed.y = float.Parse(splitData[1]);
            s_RotationSpeed.z = float.Parse(splitData[2]);
        }

        public void ItOn()
        {
            rotationSpeed = -s_RotationSpeed;
        }

        public void ItOff()
        {
            rotationSpeed = s_RotationSpeed;
        }
    }
}
