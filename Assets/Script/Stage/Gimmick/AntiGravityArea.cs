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

        GravitySwitch GravitySwitch => GravitySwitch.Instance;

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
            if (GravitySwitch != null)
            {
                GravitySwitch.Action += ChangeGravity;
            }
            ThisSprite.color = normal;
        }
        protected override void StageReset()
        {
            base.StageReset();
            if(GravitySwitch!=null) ThisSprite.color = (GravitySwitch.StartAntiGravity) ? anti : normal;
            Debug.Log("ColorChange");
        }
        public void ChangeGravity()
        {
            if (GravitySwitch != null) ThisSprite.color = (GravitySwitch.AntiGravity) ? anti : normal;
        }

        protected override void BallCheck(Ball other)
        {
            if (other.gameObject.layer == ballLayer) return;
            if(other.gameObject.TryGetComponent<IAntiGravity>(out var gravity))
            {
                gravity.ChangeGravity();
            }
        }
    }
}
