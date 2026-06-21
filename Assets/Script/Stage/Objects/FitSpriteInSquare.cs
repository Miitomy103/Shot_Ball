using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 自身のスプライトをframeの大きさに収まるように拡大縮小・位置調整するクラス。
    /// </summary>
    public class FitSpriteInSquare : MonoBehaviour, IFitSpriteInSquare
    {
        /// <summary>
        /// スプライトをframeに収まるように拡大縮小・位置調整する。
        /// </summary>
        public void FitSprite(SpriteRenderer frame)
        {
            SpriteRenderer thisSprite = GetComponent<SpriteRenderer>();
            Vector2 input = thisSprite.bounds.size;

            float max = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
            if (max == 0) transform.localScale = Vector2.zero; // 0除算防止
            transform.localScale *= frame.bounds.size.x * 0.85f / max;
            transform.position = frame.transform.position;
        }
    }
}
