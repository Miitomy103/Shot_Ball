using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Coin : AreaBase,IGet
    {
        bool get = false;
        public bool Get => get;

        protected override void BallEnter(Collider2D other)
        {
            GetCoin();
            gameObject.SetActive(false);
        }

        void GetCoin()
        {
            get = true;
        }

        protected override void BallExit(Collider2D other)
        {

        }
    }
}
