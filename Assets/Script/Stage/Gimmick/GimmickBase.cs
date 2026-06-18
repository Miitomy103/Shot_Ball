using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ギミックの基底クラス。ギミックはこのクラスを継承して作る。
    /// </summary>
    public abstract class GimmickBase : MonoBehaviour
    {
        [SerializeField] BlockType blockType;
        /// <summary>
        /// このギミックのブロックタイプ。
        /// </summary>
        public BlockType BlockType => blockType;
        /// <summary>
        /// このギミックが向きを持つかどうか。trueの場合、Directionプロパティで向きを指定する必要がある。
        /// </summary>
        protected virtual bool HasDirection => false;
        /// <summary>
        /// このギミックの向き。HasDirectionがtrueの場合のみ有効。
        /// </summary>
        protected virtual Vector2 Direction { get; }

        private SpriteRenderer thisSprite;
        protected SpriteRenderer ThisSprite => thisSprite;

        protected virtual void Awake()
        {
            ThisSpriteGet();
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
            if(GameLoop.Instance == null) return;
            GameLoop.Instance.StartAction -= StageStart;
            GameLoop.Instance.ResetAction -= StageReset;
            GameLoop.Instance.StateChangeAction -= StateChange;
        }
        /// <summary>
        /// ThisSpriteを取得する。すでに取得している場合は何もしない。
        /// </summary>
        protected void ThisSpriteGet()
        {
            if(thisSprite != null) return;
            thisSprite = GetComponent<SpriteRenderer>();
        }
        /// <summary>
        /// スタートボタンを押したときの処理。
        /// </summary>
        protected virtual void StageStart() { }
        /// <summary>
        /// リセットボタンを押したときの処理。
        /// </summary>
        protected virtual void StageReset() { }
        /// <summary>
        /// ステージの状態が変化したときの処理。ステージの状態はGameLoop.Instance.StageStateで確認できる。
        /// </summary>
        protected virtual void StateChange() { }
        /// <summary>
        /// ドラッグが開始したときの処理。
        /// </summary>
        protected virtual void DragStart() { }
        /// <summary>
        /// ドラッグしているときの処理。毎フレーム呼ばれる。
        /// </summary>
        protected virtual void Drag() { }
        /// <summary>
        /// ドラッグが終了したときの処理。
        /// </summary>
        protected virtual void DragEnd() { }
        protected Quaternion DirectionAngle()
        {
            if (!HasDirection) return Quaternion.identity;

            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(0, 0, angle - 90f);
        }
        protected virtual void Rotation() { }
        public string FrameData()=>StringData();
        /// <summary>
        /// ギミックごとに持っている特殊データを string に変換する
        /// </summary>
        protected virtual string StringData() { return null; }
        /// <summary>
        /// string の特殊データを変換する
        /// </summary>
        public virtual void LoadData(string data) { }
        /// <summary>
        /// 一部ギミックが持っている key をロードする
        /// </summary>
        public virtual void KeyNumberLoad(int[] keyNumbers) { }

        /// <summary>
        /// StageBlockDataを受け取ってギミックの状態を復元する。
        /// </summary>
        public abstract void LoadData(StageBlockData data);

        /// <summary>
        /// スプライトのアルファ値を変更する。
        /// </summary>
        protected void ColorChangeA(float a)
        {
            float colorA = Mathf.Clamp(a, 0, 1);
            Color color = ThisSprite.color;
            color.a = colorA;
            ThisSprite.color = color;
        }
        void OnDrawGizmos()
        {
            //矢印の描画
            if (!HasDirection) return;

            Vector3 start = transform.position;
            Vector3 dir3D = new Vector3(Direction.x, Direction.y, 0).normalized;
            Vector3 end = start + dir3D * 0.5f;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, end);

            // 矢印の先端を描く（三角の羽）
            Vector3 right = Quaternion.Euler(0, 0, 150) * dir3D;
            Vector3 left = Quaternion.Euler(0, 0, -150) * dir3D;

            Gizmos.DrawLine(end, end + right * 0.2f);
            Gizmos.DrawLine(end, end + left * 0.2f);
        }
    }
}
