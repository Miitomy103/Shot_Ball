using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DieBlock : CollisionBase,IOnOff
    {
        bool isOn;
        public bool IsOn => isOn;

        public DragObject DragObject => dragObject;

        public Transform Transform => transform;

        public override string Name => "DieBlock";

        DragObject dragObject;

        Collider2D colli;

        protected override void Awake()
        {
            base.Awake();
            dragObject = GetComponent<DragObject>();
            colli = GetComponent<Collider2D>();
        }
        public void ItOff()
        {
            isOn = false;
            colli.isTrigger = true;
            ColorChangeA(0.5f);
        }

        public void ItOn()
        {
            isOn = true;
            colli.isTrigger = false;
            ColorChangeA(1f);
        }
        protected override bool IsCollision()
        {
            return isOn;
        }

        protected override void BallEnter(Collision2D collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            ball.Die();
        }

    }
}
