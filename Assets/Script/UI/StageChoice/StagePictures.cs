using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class StagePictures : MonoBehaviour
    {
        const string filePath = "AreaScreenshot.";

        [SerializeField]Sprite[] stageSprites;
        public Sprite FindSpriteByName(StageName stage)
        {
            string targetName = $"{filePath}{stage.GetName()}";
            foreach (var sprite in stageSprites)
            {
                if (sprite != null && sprite.name == targetName)
                {
                    return sprite;
                }
            }
            return null;
        }
    }
}
