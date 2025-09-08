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
        [SerializeField]int keyNumber;
        [SerializeField] KeyNumberColor keyNumberColor;

        Ball ball;
        public int KeyNumber => keyNumber;

        Color color = Color.white;

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
            if (keyNumberColor != null)
            {
                ColorChange(keyNumberColor.colors[keyNumber]);
            }
        }
        void BallGenerate()
        {
            GameObject b = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            b.transform.parent = transform;
            ball = b.GetComponent<Ball>();
            ball.ColorChange(color);
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
        protected override string StringData()
        {
            return $"{isAntiGravity},{keyNumber}";

        }
        public override void LoadData(string data)
        {
            base.LoadData(data);
            string[] splitData = data.Split(',');
            isAntiGravity = bool.Parse(splitData[0]);
            keyNumber = int.Parse(splitData[1]);
        }
        IEnumerator StartCoroutine(Collider2D collider)
        {
            yield return new WaitForSeconds(0.5f);

            Physics2D.IgnoreCollision(collider, ball.collider, false); // è’ìÀÇóLå¯âª
        }

        public void ColorChange(Color color)
        {
            this.color = color;
        }

        public override void LoadData(StageBlockData data)
        {
            if(data.intParametors.TryGetValue("KeyNumber", out int keyValue))
            {
                keyNumber = keyValue;
            }
            if(data.boolParametors.TryGetValue("IsAntiGravity", out bool antiGravityValue))
            {
                isAntiGravity = antiGravityValue;
            }
        }
    }
}
