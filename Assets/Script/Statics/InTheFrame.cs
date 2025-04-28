using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class InTheFrame 
    {
        public static Vector2 NormalizeMaxToValue(Vector2 input, float targetMax)
        {
            float max = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
            if (max == 0) return Vector2.zero; // 0èúéZñhé~
            return input / max * targetMax;
        }
    }
}
