using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
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
