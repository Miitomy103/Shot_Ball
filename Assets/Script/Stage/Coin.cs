using ShotBall.InGame.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Coin : AreaBase,IGet
    {
        bool get = false;
        public bool Get => get;

        public override string Name => "Coin";

        readonly float time = 1f;

        CoinAnimation anim;

        Vector3 startPos;

        protected override void Awake()
        {
            base.Awake();
            anim = GetComponent<CoinAnimation>();
        }
        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
        }
        protected override void StageReset()
        {
            base.StageReset();
            
            get = false;

            gameObject.SetActive(true);
            anim.RotateStop();
            transform.position = startPos;
        }

        protected override void BallEnter(Collider2D other)
        {
            if (Get) return;
            GetCoin();
        }

        void GetCoin()
        {
            get = true;

            anim.Rotate(3, GetAfter);
        }
        void GetAfter()
        {
            gameObject.SetActive(false);
        }
    }
}
