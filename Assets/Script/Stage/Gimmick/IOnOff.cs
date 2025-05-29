using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public interface IOnOff
    {
        bool IsOn { get;}
        public Transform Transform { get; }
        public DragObject DragObject { get; }
        void ItOn();
        void ItOff();
    }
}
