using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DieBlock : CollisionBase,IOnOff
    {
        bool isOn = true;
        public bool IsOn => isOn;

        public DragObject DragObject => dragObject;

        public Transform Transform => transform;


        DragObject dragObject;

        protected override void Awake()
        {
            base.Awake();
            dragObject = GetComponent<DragObject>();
        }
        public void ItOff()
        {
            isOn = false;
            Colli.isTrigger = true;
            ColorChangeA(0.5f);
        }

        public void ItOn()
        {
            isOn = true;
            Colli.isTrigger = false;
            ColorChangeA(1f);
        }
        protected override bool IsCollision()
        {
            return isOn;
        }

        protected override void BallEnter(Collision2D collision)
        {
            Debug.Log("B");
            Ball ball = collision.gameObject.GetComponent<Ball>();
            ball.Die();
        }

        public override void LoadData(StageBlockData data)
        {

        }
    }
}
