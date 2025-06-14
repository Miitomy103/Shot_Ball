using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class GimmickBase : MonoBehaviour
    {
        public abstract string Name { get; }
        protected virtual bool HasDirection => false;
        protected virtual Vector2 Direction { get; }

        private SpriteRenderer thisSprite;
        protected SpriteRenderer ThisSprite => thisSprite;

        protected virtual void Awake()
        {
            thisSprite = GetComponent<SpriteRenderer>();
            if (TryGetComponent<DragObject>(out var drag))
            {
                drag.DragStartAction += DragStart;
                drag.DragAction += Drag;
                drag.DragEndAction += DragEnd;
            }
        }
        protected virtual void Start()
        {
            GameLoop.Instance.StartAction += StageStart;
            GameLoop.Instance.ResetAction += StageReset;
            GameLoop.Instance.StateChangeAction += StateChange;
            if(TryGetComponent<RotationDragObject>(out var r))
            {
                r.Action += Rotation;
            }

        }
        private void OnDestroy()
        {
            GameLoop.Instance.StartAction -= StageStart;
            GameLoop.Instance.ResetAction -= StageReset;
            GameLoop.Instance.StateChangeAction -= StateChange;
        }
        protected void ThisSpriteGet()
        {
            thisSprite = GetComponent<SpriteRenderer>();
        }
        protected virtual void StageStart() { }
        protected virtual void StageReset() { }
        protected virtual void StateChange() { }
        protected virtual void DragStart() { }
        protected virtual void Drag() { }
        protected virtual void DragEnd() { }
        protected Quaternion DirectionAngle()
        {
            if (!HasDirection) return Quaternion.identity;

            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(0, 0, angle - 90f);
        }
        protected virtual void Rotation() { }
        public string FrameData()
        {
            //string sizeData = ThisSprite.size.ToString();
            //string scaleData = transform.localScale.ToString();
            //Collider2D col = GetComponent<Collider2D>();
            //string colliderTypeName = col.GetType().Name;
            //return Name+sizeData+scaleData+colliderTypeName + StringData();
            return StringData();
        }
        protected virtual string StringData() { return string.Empty; }
        public virtual void LoadData(string data) { }
        protected void ColorChangeA(float a)
        {
            float colorA = Mathf.Clamp(a, 0, 1);
            Color color = ThisSprite.color;
            color.a = colorA;
            ThisSprite.color = color;
        }
        protected string[] Sprit(string data)
        {
            return data.Split(',');
        }
        void OnDrawGizmos()
        {
            if (!HasDirection) return;

            Vector3 start = transform.position;
            Vector3 dir3D = new Vector3(Direction.x, Direction.y, 0).normalized;
            Vector3 end = start + dir3D * 0.5f;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, end);

            // ñÓàÛÇÃêÊí[Çï`Ç≠ÅiéOäpÇÃâHÅj
            Vector3 right = Quaternion.Euler(0, 0, 150) * dir3D;
            Vector3 left = Quaternion.Euler(0, 0, -150) * dir3D;

            Gizmos.DrawLine(end, end + right * 0.2f);
            Gizmos.DrawLine(end, end + left * 0.2f);
        }
    }
}
