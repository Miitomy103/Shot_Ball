using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 特に何も起きないただのブロック
    /// </summary>
    public class NormalBlock : CollisionBase, IOnOff
    {
        [SerializeField] AudioSource hitSound;

        bool isOn = true;
        public bool IsOn => isOn;

        public Transform Transform => transform;

        public void ItOff()
        {
            ColorChangeA(0.5f);
            isOn = false;
            Collider2D.isTrigger = !isOn;
        }

        public void ItOn()
        {
            ColorChangeA(1);
            isOn = true;
            Collider2D.isTrigger = !isOn;
        }

        public override void LoadData(StageBlockData data)
        {

        }

        protected override void BallEnter(Collision2D collision)
        {
            hitSound.Play();
            Debug.Log($"hitSound");
        }
        protected override bool IsCollision()
        {
            return isOn;
        }
    }
}
