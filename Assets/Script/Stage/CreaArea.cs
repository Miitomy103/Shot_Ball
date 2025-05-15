using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CreaArea : AreaBase
    {

        protected override void BallEnter(Collider2D other)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.Clear();
        }
    }
}

