using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class WindArea : AreaBase,IOnOff
    {
        [SerializeField] float windPower = 5f;
        [SerializeField] Vector2 windDirection;
        Vector2 reverseDrection;
        [SerializeField] RectTransform backArrow;

        protected override bool HasDirection => true;
        protected override Vector2 Direction => windDirection;

        public override string Name => "WindArea";

        bool isOn;
        public bool IsOn => isOn;

        public Transform Transform => transform;

        public DragObject DragObject => throw new System.NotImplementedException();

        List<Rigidbody2D> rigidBodies = new List<Rigidbody2D>();

        protected override void Start()
        {
            backArrow.rotation = DirectionAngle();
            reverseDrection = -Direction;
        }
        private void FixedUpdate()
        {
            if (rigidBodies.Count == 0) return;
            foreach(Rigidbody2D rb in rigidBodies)
            {
                Vector2 d = isOn ? reverseDrection : Direction;
                rb.AddForce(d.normalized * windPower);
            }
        }
        protected override void BallEnter(Ball   other)
        {
            if(other.gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                rigidBodies.Add(rigidbody2D);
            }
        }
        protected override void BallExit(Ball other)
        {
            if (other.gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                rigidBodies.Remove(rigidbody2D);
            }
        }

        protected override string StringData()
        {
            return windPower.ToString();
        }

        public void ItOn()
        {
            isOn = true;
        }

        public void ItOff()
        {
            isOn = false;
        }
    }
}
