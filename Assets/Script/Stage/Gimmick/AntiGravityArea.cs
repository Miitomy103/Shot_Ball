using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class AntiGravityArea : AreaBaseTogether
    {
        protected override void BallCheck(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer) return;
            if(other.gameObject.TryGetComponent<IAntiGravity>(out var gravity))
            {
                gravity.ChangeGravity();
            }
        }
    }
}
