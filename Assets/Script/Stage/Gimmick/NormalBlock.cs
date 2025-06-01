using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class NormalBlock : CollisionBase
    {
        [SerializeField] AudioSource hitSound;

        public override string Name => "NormalBlock";

        protected override void BallEnter(Collision2D collision)
        {
            hitSound.Play();
        }
    }
}
