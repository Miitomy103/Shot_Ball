using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class ItemBase : AreaBase
    {
        protected override void BallEnter(Collider2D other)
        {
            base.BallEnter(other);
            Ball ball = other.GetComponent<Ball>();
            if(ball!=null)
            {
                GetItem(ball);
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
