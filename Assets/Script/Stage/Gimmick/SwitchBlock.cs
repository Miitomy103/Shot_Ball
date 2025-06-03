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

        public override string Name => "SwitchBlock";

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
            Colli.isTrigger = true;
            ColorChangeA(0.5f);
        }

        public void ItOff()
        {
            isEnter = false;
            Colli.isTrigger = false;
            ColorChangeA(1);
        }
    }
}
