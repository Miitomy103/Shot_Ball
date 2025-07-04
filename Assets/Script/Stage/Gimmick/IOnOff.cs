using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public interface IOnOff
    {
        bool IsOn { get;}
        void ItOn();
        void ItOff();
        //TODO:StringLoad‚Å‚«‚é‚æ‚¤‚É‚·‚é
    }
}
