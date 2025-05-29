using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public interface IAntiGravity
    {
        public void ChangeGravity();

        public bool AntiGravity { get; }

    }
}
