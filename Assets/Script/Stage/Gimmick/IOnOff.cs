using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public interface IOnOff
    {
        bool IsOn { get;}
        public Transform Transform { get; }
        void ItOn();
        void ItOff();
    }
}
