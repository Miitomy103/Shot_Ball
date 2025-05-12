using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public static class TransformCalculation
    {
        public static void SetPositionXY(Transform transform, Vector2 xy)
        {
            transform.position = new Vector3(xy.x, xy.y, transform.position.z);
        }
    }
}
