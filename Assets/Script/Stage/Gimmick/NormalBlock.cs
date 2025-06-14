using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class NormalBlock : CollisionBase, IOnOff
    {
        [SerializeField] AudioSource hitSound;

        public override string Name => "NormalBlock";

        bool isOn = true;
        public bool IsOn => isOn;

        public Transform Transform => transform;

        public void ItOff()
        {
            ColorChangeA(0.5f);
            isOn = false;
            Colli.isTrigger = !isOn;
        }

        public void ItOn()
        {
            ColorChangeA(1);
            isOn = true;
            Colli.isTrigger = !isOn;
        }

        protected override void BallEnter(Collision2D collision)
        {
            hitSound.Play();
            Debug.Log($"hitSound");
        }
        protected override bool IsCollision()
        {
            return isOn;
        }
    }
}
