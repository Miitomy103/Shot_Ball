using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class SwitchBlock : CollisionBase,IOnOff
    {
        bool isEnter = false;

        [SerializeField] AudioSource switchSound;

        bool isOn;
        public bool IsOn => isOn;

        public Transform Transform => transform;

        public DragObject DragObject => throw new System.NotImplementedException();


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
            Collider2D.isTrigger = true;
            ColorChangeA(0.5f);
        }

        public void ItOff()
        {
            isEnter = false;
            Collider2D.isTrigger = false;
            ColorChangeA(1);
        }

        public override void LoadData(StageBlockData data)
        {

        }
    }
}
