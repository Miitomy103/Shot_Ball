using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class TimeStop : ItemBase
    {
        [SerializeField] float time = 1;
        protected override void GetItem(Ball ball)
        {
            ball.TimeStop(time);   
        }
    }
}
