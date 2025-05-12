using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class AreaBase : GimmickBase
    {
        [SerializeField]protected LayerMask ballLayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer) return;
            BallEnter(other);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer) return;
            BallExit(other);
        }
        protected abstract void BallEnter(Collider2D other);
        protected abstract void BallExit(Collider2D other);
    }
}
