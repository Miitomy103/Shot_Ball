using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// BoxCollider2D用にサイズを反映するSpriteImage。
    /// </summary>
    public class BoxSpriteImage : SpriteImage
    {
        Vector2 originSize;
        protected override void ColliderSizeChange()
        {
            if(colli == null)
            {
                Debug.LogError("colliNone");
                return;
            }
            if (colli is BoxCollider2D box)
            {
                box.size = size;
            }
            else
            {
                //Debug.LogError("boxNone");
            }
        }
    }
}
