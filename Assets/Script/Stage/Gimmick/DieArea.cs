using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DieArea : AreaBaseTogether
    {
        protected override void BallCheck(Collider2D other)
        {
            Ball ball = other.GetComponent<Ball>();
            ball.Die();
        }
    }
}
