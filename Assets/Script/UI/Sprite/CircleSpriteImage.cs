using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CircleSpriteImage : SpriteImage
    {
        Vector2 origin = Vector2.one;
        protected override void ColliderSizeChange()
        {
            if(size.x!=origin.x)
            {
                size.y = size.x;
            }
            else
            {
                size.x = size.y;
            }
            origin = size;

            if (colli is CircleCollider2D circle)
            {
                circle.radius = size.x / 2;
            }
        }
    }
}
