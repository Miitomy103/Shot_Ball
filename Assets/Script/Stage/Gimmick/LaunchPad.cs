using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class LaunchPad : GimmickBase
    {
        [SerializeField] GameObject ballPrefab;

        Ball ball;
        protected override void Start()
        {
            base.Start();
            GameObject b = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball = b.GetComponent<Ball>();
        }
        protected override void StageStart()
        {
            Debug.Log("Start");
            Collider2D collider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(collider, ball.collider, true); // è’ìÀÇñ≥å¯âª
            StartCoroutine(StartCoroutine(collider));
            ball.PlayStart();
        }
        protected override void StageReset()
        {
            if (ball != null)
            {
                Destroy(ball.gameObject);
                ball = null;
            }
            GameObject b = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball = b.GetComponent<Ball>();
        }
        IEnumerator StartCoroutine(Collider2D collider)
        {
            yield return new WaitForSeconds(0.5f);

            Physics2D.IgnoreCollision(collider, ball.collider, false); // è’ìÀÇóLå¯âª
        }
    }
}
