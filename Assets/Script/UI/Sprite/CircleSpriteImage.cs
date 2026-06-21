using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// CircleCollider2D用に、サイズ変更時にX/Yの一方の値を採用して正円を維持しつつ半径を反映するSpriteImage。
    /// </summary>
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
