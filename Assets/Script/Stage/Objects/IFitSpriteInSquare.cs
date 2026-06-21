using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// スプライトを指定の枠(frame)に収まるように調整できることを表すインターフェース。
    /// </summary>
    public interface IFitSpriteInSquare
    {
        /// <summary>
        /// スプライトをframeに収まるように拡大縮小・位置調整する。
        /// </summary>
        void FitSprite(SpriteRenderer frame);
    }
}