using ShotBall.InGame.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 取得できるコインのギミック。ボールが触れると取得済みになり非表示になる。
    /// </summary>
    public class Coin : AreaBase,IGet
    {
        bool get = false;
        public bool IsGet => get;


        static float pitch = 1;

        readonly float time = 1f;

        [SerializeField] AudioSource enterSound;

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
        protected override void OrderChange()
        {
            ThisSprite.sortingOrder = (int)OrderInLayer.Coin;
        }
        protected override void StageStart()
        {
            base.StageStart();
            pitch = 1;
        }
        protected override void StageReset()
        {
            base.StageReset();
            
            get = false;

            gameObject.SetActive(true);
            anim.RotateStop();
            transform.position = startPos;

            pitch = 1;
        }

        protected override void BallEnter(Ball other)
        {
            if (IsGet) return;
            GetCoin();

        }

        void GetCoin()
        {
            get = true;

            anim.Rotate(3, GetAfter);

            enterSound.pitch = pitch;
            enterSound.Play();
            pitch += 0.2f;
        }
        void GetAfter()
        {
            gameObject.SetActive(false);
        }

        public override void LoadData(StageBlockData data)
        {
        }
    }
}
