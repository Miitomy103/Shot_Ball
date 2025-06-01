using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class AntiGravityArea : AreaBaseTogether,IAntiGravity
    {
        bool antiGravity;
        public bool AntiGravity => antiGravity;
        Color normal;
        Color anti => Camera.main.backgroundColor;

        public override string Name => "AntiGravityArea";

        [SerializeField] AudioSource antiGravitySound;

        protected override void Awake()
        {
            base.Awake();
            normal = ThisSprite.color;
        }

        protected override void Start()
        {
            base.Start();
            if (GravitySwitch.Instance != null)
            {
                GravitySwitch.Instance.Action += ChangeGravity;
            }
            ThisSprite.color = normal;
        }
        protected override void StageReset()
        {
            base.StageReset();
            ThisSprite.color = (GravitySwitch.Instance.StartAntiGravity) ? anti : normal;
            Debug.Log("ColorChange");
        }
        public void ChangeGravity()
        {
            ThisSprite.color = (GravitySwitch.Instance.AntiGravity) ? anti : normal;
        }

        protected override void BallCheck(Collider2D other)
        {
            if (other.gameObject.layer == ballLayer) return;
            if(other.gameObject.TryGetComponent<IAntiGravity>(out var gravity))
            {
                gravity.ChangeGravity();
            }
        }
    }
}
