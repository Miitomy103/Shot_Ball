using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class SwitchBlock : CollisionBase,IOnOff
    {
        bool isEnter = false;

        private Collider2D colli;

        [SerializeField] AudioSource switchSound;

        bool isOn;
        public bool IsOn => isOn;

        public Transform Transform => transform;

        public DragObject DragObject => throw new System.NotImplementedException();

        protected override void Start()
        {
            base.Start();
            colli = GetComponent<Collider2D>();
        }
        protected override void BallEnter(Collision2D collision)
        {
            base.BallEnter(collision);
            ItOn();
            switchSound.Play();
        }
        protected override void StageReset()
        {
            base.StageReset();
            ItOff();
        }

        public void ItOn()
        {
            isEnter = true;
            colli.isTrigger = true;
            ColorChangeA(0.5f);
        }

        public void ItOff()
        {
            isEnter = false;
            colli.isTrigger = false;
            ColorChangeA(1);
        }
    }
}
