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

        [SerializeField] AudioSource reroad;
        [SerializeField] AudioSource launchSound;
        protected override void Start()
        {
            Transform child = transform.Find("Arrow");
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
            rb.AddForce(launchPower*10 * Direction);
            launchSound.Play();
        }

    }
}
