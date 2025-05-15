using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class WindArea : AreaBase
    {
        [SerializeField] float windPower = 5f;
        [SerializeField] Vector2 windDirection;
        [SerializeField] RectTransform backArrow;

        protected override Vector2 Direction => windDirection;

        List<Rigidbody2D> rigidBodies = new List<Rigidbody2D>();

        protected override void Start()
        {
            backArrow.rotation = DirectionAngle();
        }
        private void FixedUpdate()
        {
            if (rigidBodies.Count == 0) return;
            foreach(Rigidbody2D rb in rigidBodies)
            {
                rb.AddForce(Direction.normalized * windPower);
            }
        }
        protected override void BallEnter(Collider2D other)
        {
            if(other.gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                rigidBodies.Add(rigidbody2D);
            }
        }
        protected override void BallExit(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2D))
            {
                rigidBodies.Remove(rigidbody2D);
            }
        }
    }
}
