using UnityEngine;

namespace ShotBall.InGame
{
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
        protected abstract void GetItem(Ball ball);
    }
}
