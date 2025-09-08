using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class FitSpriteInSquare : MonoBehaviour, IFitSpriteInSquare
    {
        public void FitSprite(SpriteRenderer frame)
        {
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            Vector2 input = thisSprite.bounds.size;

            float max = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
            if (max == 0) transform.localScale = Vector2.zero; // 0èúéZñhé~
            transform.localScale *= frame.bounds.size.x * 0.85f / max;
            transform.position = frame.transform.position;
        }
    }
}
