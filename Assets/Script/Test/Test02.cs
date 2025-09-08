using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class Test02<T> 
    {
        public T Speed;
        public T Smart;

        public Test02(T speed, T smart)
        {
            Speed = speed;
            Smart = smart;
        }
    }
}
