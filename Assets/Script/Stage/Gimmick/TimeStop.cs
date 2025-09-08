using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

        protected override string StringData()
        {
            return $"{time}";
        }
        public override void LoadData(string data)
        {
            base.LoadData(data);
            time = float.Parse(data);
        }

        public override void LoadData(StageBlockData data)
        {
            if (data.floatParametors.TryGetValue("Time", out float timeValue))
            {
                time = timeValue;
            }
            else
            {
                Debug.LogWarning("Time parameter not found in TimeStop data.");
            }
        }
    }
}
