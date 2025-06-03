using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class BallLauncher : AreaBase
    {
        [SerializeField] float launchPower = 40f;
        [SerializeField] Vector2 launchDirection;
        float coolTime = 0.5f;

        protected override bool HasDirection => true;
        protected override Vector2 Direction => launchDirection;

        public override string Name => "BallLauncher";

        bool inBall;
        Transform child;

        [SerializeField] AudioSource reroad;
        [SerializeField] AudioSource launchSound;
        protected override void Start()
        {
            child = transform.Find("Arrow");
            child.localRotation = DirectionAngle();
            base.Start();
        }
        protected override void BallEnter(Collider2D other)
        {
            base.BallEnter(other);
            if (inBall) return;
            Debug.Log("AA");
            StartCoroutine(Coroutine(other.gameObject.GetComponent<Rigidbody2D>()));
        }
        protected override void Rotation()
        {
            base.Rotation();
            child.localRotation = DirectionAngle();
        }
        IEnumerator Coroutine(Rigidbody2D rb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            inBall = true;
            rb.velocity = Vector2.zero;
            rb.gameObject.transform.position = transform.position;
            reroad.Play();

            yield return new WaitForSeconds(coolTime);

            inBall = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Vector2 worldForce = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Direction;
            rb.AddForce(launchPower*10 * worldForce);
            launchSound.Play();
        }

    }
}
