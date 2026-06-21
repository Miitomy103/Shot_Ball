using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// BoxCollider2D用にサイズを反映するSpriteImages。
    /// </summary>
    public class BoxSpriteImages : SpriteImages
    {
        protected override void ColliderSizeChange()
        {
            if (colli is BoxCollider2D box)
            {
                box.size = size;
            }
            else
            {
                Debug.LogError("boxNone");
            }
        }
    }
}
