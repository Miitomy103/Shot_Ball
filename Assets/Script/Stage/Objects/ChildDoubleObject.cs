using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace ShotBall.InGame
{
    public class ChildDoubleObject : InFrameObject
    {
        SpriteRenderer[] children;
        private void Start()
        {
            children = GetComponentsInChildren<SpriteRenderer>();
        }

        protected override void FrameItObject(SpriteRenderer frameSprite)
        {
            foreach(SpriteRenderer child in children)
            {
                child.transform.position = Vector3.zero;
                FitSpriteInSquare(frameSprite, child);
            }
        }
    }
}
