using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class AreaBase : MonoBehaviour
    {
        [SerializeField]protected LayerMask ballLayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer) return;
            BallCheck(other);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer) return;
            BallCheck(other);
        }
        public abstract void BallCheck(Collider2D other);
    }
}
