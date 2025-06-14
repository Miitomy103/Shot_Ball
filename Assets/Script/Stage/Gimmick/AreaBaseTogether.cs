using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class AreaBaseTogether : AreaBase
    {
        protected override void BallEnter(Ball other)
        {
            BallCheck(other);
        }

        protected override void BallExit(Ball other)
        {
            BallCheck(other);
        }

        protected abstract void BallCheck(Ball other);
    }
}
