using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace ShotBall.InGame
{
    public class RuntimeDragObject :DragObject
    {
        protected Collider2D colli;

        [SerializeField] readonly Color backColor = new Color(0.1f, 0.1f, 0.1f, 0.75f);
        readonly Color outColor = new Color(1f, 0, 0, 0.85f);

        SubSprite backSprite;
        SubSprite outSprite;

        public Action DragAction { get; set; }
        protected SpriteRenderer thisSprite { get; private set; }

        private void OnEnable()
        {
            thisSprite = GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            inFrame = true;

            otherPosition = transform.position;

            backSprite = new SubSprite(transform, thisSprite, backColor, 1.1f);
            outSprite = new SubSprite(transform, thisSprite, outColor);
            OrderInLayerChange(placeOrder);


            backSprite.gameObject.SetActive(false);
            outSprite.gameObject.SetActive(false);
        }


        protected override void Awake()
        {
            base.Awake();
            if (thisSprite == null) thisSprite = GetComponent<SpriteRenderer>();
            transform.position += new Vector3(0, 0, -0.0f);
            colli = GetComponent<Collider2D>();

            GimmickBase gimmick = GetComponent<GimmickBase>();
            if(gimmick!=null) Name = gimmick.Name;
        }

        private void OnMouseEnter()
        {
            if (!CanDrag()) return;
            backSprite.gameObject.SetActive(true);
        }
        private void OnMouseExit()
        {
            if (!CanDrag()) return;
            backSprite.gameObject.SetActive(false);
        }
        protected virtual void OnMouseDown()
        {
            if (!CanDrag()) return;
            StartDrag(Input.mousePosition);
        }

        protected virtual void OnMouseUp()
        {
            if (!CanDrag()) return;
            EndDrag(Input.mousePosition);
        }
        /// <summary>
        /// íuÇØÇÈÇ©íuÇØÇ»Ç¢Ç©
        /// </summary>
        public override bool IsPlaced()
        {
            if (!ObjectRange.Instance.InRange(thisSprite) || IsOverLapping())
            {
                if (IsFramePositionAvailable())
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        protected override void StartDragDerivation(Vector3 inputScreenPos)
        {
            if (backSprite == null) return;
            backSprite.transform.localScale *= 1.05f;
        }

        protected override void DragDerivation(Vector3 inputScreenPos)
        {
            if (IsPlaced())
            {
                outSprite.gameObject.SetActive(false);
            }
            else
            {
                outSprite.gameObject.SetActive(true);
            }
        }

        protected override void EndDragDerivation(Vector3 inputScreenPos)
        {
            backSprite.transform.localScale /= 1.05f;
        }
        /// <returns>îÌÇ¡ÇƒÇ¢ÇÈ=>true</returns>
        protected override bool IsOverLapping()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = false; // Trigger Ç‡ä‹ÇﬂÇΩÇ¢èÍçáÇÕ true Ç…
            filter.SetLayerMask(Physics2D.DefaultRaycastLayers); // ëSÉåÉCÉÑÅ[ëŒè€Ç…Ç∑ÇÈ

            List<Collider2D> results = new List<Collider2D>();
            int count = colli.OverlapCollider(filter, results);

            return count > 0;
        }
        protected override void OrderInLayerChange(int order)
        {
            thisSprite.sortingOrder = order;
            if (backSprite == null || outSprite == null) return;
            backSprite.LayerChange(order - 1);
            outSprite.LayerChange(order + 1);
        }
        public void OnDestroy()
        {
            foreach(Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        protected override bool CanDrag()
        {
            if (GameLoop.StageState != StageState.Setting) return false;
            return true;
        }

        protected override bool InRange()
        {
            return ObjectRange.Instance.InRange(thisSprite);
        }
        public override void FitSpriteInSquare(SpriteRenderer frame)
        {
            Vector2 input = thisSprite.bounds.size;

            float max = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
            if (max == 0) transform.localScale = Vector2.zero; // 0èúéZñhé~
            transform.localScale *= frame.bounds.size.x * 0.85f / max;
            transform.position = frame.transform.position;
        }
    }
}
