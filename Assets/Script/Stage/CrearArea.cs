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

        [SerializeField] KeyNumberColor keyNumberColor;

        [SerializeField,ReadOnly] Color currentColor = Color.white;

        public int KeyNumber => keyNumber;

        static float pitch = 1;

        private void OnValidate()
        {
            if (keyNumberColor != null)
            {
                ColorChange(keyNumberColor.colors[keyNumber]);
            }
        }
        protected override void StageStart()
        {
            base.StageStart();
            pitch = 1;

            if (keyNumberColor != null)
            {
                ColorChange(keyNumberColor.colors[keyNumber]);
            }
        }
        public void ColorChange(Color color)
        {
            //ThisSprite‚ªnull‚Ìê‡‚ª‚ ‚é‚Ì‚ÅAˆê‰æ“¾
            ThisSpriteGet();
            ThisSprite.color = color;
            currentColor = color;
        }
        protected override string StringData()
        {
            return $"{keyNumber}";
        }
        public override void LoadData(string data)
        {
            base.LoadData(data);
            keyNumber = int.Parse(data);
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

        public override void LoadData(StageBlockData data)
        {
            if(data.intParametors.TryGetValue("KeyNumber", out int keyValue))
            {
                keyNumber = keyValue;
            }
            else
            {
                Debug.LogWarning("KeyNumber parameter not found in CrearArea data.");
            }
        }
    }
}

