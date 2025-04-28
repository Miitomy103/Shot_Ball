using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class AntiGravityArea : MonoBehaviour
    {
        [SerializeField]LayerMask ballLayer;
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
        public void BallCheck(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer) return;
            if(other.gameObject.TryGetComponent<IAntiGravity>(out var gravity))
            {
                gravity.ChangeGravity();
            }
        }
    }
}
