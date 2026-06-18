using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ボールのスタート位置
    /// </summary>
    public class LaunchPad : GimmickBase,IKeyNumber
    {
        [SerializeField] GameObject ballPrefab;
        [SerializeField] bool isAntiGravity;
        [SerializeField,Tooltip("ゴールのIDを指定する")] int keyNumber;
        [SerializeField,Tooltip("ゴールのIDに合わせて色を変える")] KeyNumberColor keyNumberColor;
        [SerializeField, ReadOnly] Color currentColor = Color.white;
        Ball ball;
        public int KeyNumber => keyNumber;

        Color color = Color.white;

        protected override void Awake()
        {
            base.Awake();
            if (keyNumberColor != null)
            {
                ColorChange(keyNumberColor.colors[keyNumber]);
            }
        }
        private void OnValidate()
        {
            if (keyNumberColor != null)
            {
                ColorChange(keyNumberColor.colors[keyNumber]);
            }
        }
        protected override void Start()
        {
            base.Start();
            BallGenerate();
        }
        public void ColorChange(Color color)
        {
            this.color = color;
            currentColor = color;
        }

        /// <summary>
        /// 発射
        /// </summary>
        protected override void StageStart()
        {
            Debug.Log("Start");
            Collider2D collider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(collider, ball.Collider2d, true); // プレイヤー関連
            StartCoroutine(StartCoroutine(collider));
            ball.PlayStart(KeyNumber);
            if (keyNumberColor != null)
            {
                ColorChange(keyNumberColor.colors[keyNumber]);
            }
        }
        /// <summary>
        /// ボール生成
        /// </summary>
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
            //ボールが離れてから判定を復活させる
            yield return new WaitForSeconds(0.5f);

            Physics2D.IgnoreCollision(collider, ball.Collider2d, false); //プレイヤーキー
        }


        public override void LoadData(StageBlockData data)
        {
            if(data.intParameters.TryGetValue("KeyNumber", out int keyValue))
            {
                keyNumber = keyValue;
            }
            if(data.boolParameters.TryGetValue("IsAntiGravity", out bool antiGravityValue))
            {
                isAntiGravity = antiGravityValue;
            }
        }
    }
}
