using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ShotBall.InGame
{
    public class LaunchPad : GimmickBase,IKeyNumber
    {
        [SerializeField] GameObject ballPrefab;
        [SerializeField] bool isAntiGravity;

        Ball ball;
        public int KeyNumber{ get;set;  }

        Color color;

        protected override void Start()
        {
            base.Start();
            BallGenerate();
        }
        protected override void StageStart()
        {
            Debug.Log("Start");
            Collider2D collider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(collider, ball.collider, true); // è’ìÀÇñ≥å¯âª
            StartCoroutine(StartCoroutine(collider));
            ball.PlayStart(KeyNumber);
        }
        void BallGenerate()
        {
            GameObject b = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball = b.GetComponent<Ball>();
            if (isAntiGravity) ball.ChangeGravity();
        }
        protected override void StageReset()
        {
            if (ball != null)
            {
                Destroy(ball.gameObject);
                ball = null;
            }
            BallGenerate();
        }
        IEnumerator StartCoroutine(Collider2D collider)
        {
            yield return new WaitForSeconds(0.5f);

            Physics2D.IgnoreCollision(collider, ball.collider, false); // è’ìÀÇóLå¯âª
        }

        public void ColorChange(Color color)
        {
            this.color = color;
            ball.ColorChange(color);
        }
    }
}
