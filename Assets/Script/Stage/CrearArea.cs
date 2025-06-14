using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CrearArea : AreaBase,IKeyNumber
    {
        [SerializeField] int keyNumber;

        [SerializeField] AudioSource clearSound;

        public int KeyNumber { get;set; }

        public override string Name => "CreaArea";

        static float pitch = 1;

        protected override void StageStart()
        {
            base.StageStart();
            pitch = 1;
        }
        public void ColorChange(Color color)
        {
            if(ThisSprite == null) ThisSpriteGet();
            ThisSprite.color = color;
        }
        protected override string StringData()
        {
            return KeyNumber.ToString();
        }
        public override void LoadData(string data)
        {
            base.LoadData(data);
            KeyNumber = int.Parse(data);
        }
        protected override void BallEnter(Ball other)
        {
            Debug.Log("Hit");
            if(KeyNumber==other.KeyNumber)
            {
                other.Clear();
                clearSound.pitch = pitch;
                clearSound.Play();
                pitch += 0.2f;
            }
        }
    }
}

