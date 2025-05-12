using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class AreaBaseTogether : AreaBase
    {
        protected override void BallEnter(Collider2D other)
        {
            BallCheck(other);
        }

        protected override void BallExit(Collider2D other)
        {
            BallCheck(other);
        }

        protected abstract void BallCheck(Collider2D other);
    }
}
