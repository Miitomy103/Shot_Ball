using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class WindArea : MonoBehaviour
    {
        [SerializeField] LayerMask ballLayer;
        [SerializeField] float windPower = 5f;
        [SerializeField] Vector2 windDirection;

        List<Rigidbody2D> rigidBodies = new List<Rigidbody2D>();

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
        private void FixedUpdate()
        {
            if (rigidBodies.Count == 0) return;
            foreach(Rigidbody2D rb in rigidBodies)
            {
                rb.AddForce(windDirection.normalized * windPower);
            }
        }
        void BallEnter(Collider2D other)
        {
            if(other.gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                rigidBodies.Add(rigidbody2D);
            }
        }
        void BallExit(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                rigidBodies.Remove(rigidbody2D);
            }
        }
    }
}
