using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CrearArea : AreaBase,IKeyNumber
    {
        [SerializeField] int keyNumber;

        public int KeyNumber { get;set; }

        public override string Name => "CreaArea";

        public void ColorChange(Color color)
        {
            if(ThisSprite == null) ThisSpriteGet();
            ThisSprite.color = color;
        }

        protected override void BallEnter(Collider2D other)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if(KeyNumber==ball.KeyNumber)
            {
                ball.Clear();
            }
        }
    }
}

