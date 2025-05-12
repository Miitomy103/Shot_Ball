using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class BallLauncher : GimmickBase
    {
        [SerializeField] float launchPower = 40f;
        [SerializeField] Vector2 launchDirection;
        float coolTime = 0.5f;
        [SerializeField] LayerMask ballLayer;

        protected override Vector2 Direction => launchDirection;

        bool inBall;
        private void Start()
        {
            Transform child = transform.Find("Arrow");
            child.localRotation = DirectionAngle();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer||inBall) return;
            Debug.Log("AA");
            StartCoroutine(Coroutine(other.gameObject.GetComponent<Rigidbody2D>()));
        }
        IEnumerator Coroutine(Rigidbody2D rb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            inBall = true;
            rb.velocity = Vector2.zero;
            rb.gameObject.transform.position = transform.position;
            yield return new WaitForSeconds(coolTime);
            inBall = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(launchPower*10 * Direction);
        }

    }
}
