using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ボールが触れると取得され非表示になるアイテムの基底クラス。
    /// </summary>
    public abstract class ItemBase : AreaBase
    {
        protected override void BallEnter(Ball other)
        {
            base.BallEnter(other);
            if(other!=null)
            {
                GetItem(other);
                gameObject.SetActive(false);
            }
        }
        protected override void StageReset()
        {
            base.StageReset();
            gameObject.SetActive(true);
        }
        /// <summary>
        /// アイテムを取得したときの効果。
        /// </summary>
        protected abstract void GetItem(Ball ball);
    }
}
