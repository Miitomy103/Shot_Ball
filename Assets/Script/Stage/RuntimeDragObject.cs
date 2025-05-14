using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class RuntimeDragObject : DragObject
    {
        bool isDragging = false;
        public bool placed;
        private Vector3 offset;
        Collider2D colli;

        private Vector3 otherPosition;
        Vector3 originalSize;

        public bool inFrame { get;private set; }
        Frame frameScr;

        const int placeOrder = 3;
        const int dragOrder = 7;

        [SerializeField] readonly Color backColor = new Color(0.1f, 0.1f, 0.1f, 0.75f);
        readonly Color outColor = new Color(1f, 0, 0, 0.85f);

        SubSprite backSprite;
        SubSprite outSprite;


        private void Start()
        {
            otherPosition = transform.position;

            backSprite = new SubSprite(transform, thisSprite, backColor, 1.1f);
            outSprite = new SubSprite(transform, thisSprite, outColor);
            OrderInLayerChange(placeOrder);


            backSprite.gameObject.SetActive(false);
            outSprite.gameObject.SetActive(false);
        }


        protected override void Awake()
        {
            transform.position += new Vector3(0, 0, -0.1f);
            originalSize = transform.localScale;
            colli = GetComponent<Collider2D>();
        }

        private void OnMouseEnter()
        {
            backSprite.gameObject.SetActive(true);
        }
        private void OnMouseExit()
        {
            backSprite.gameObject.SetActive(false);
        }
        protected virtual void OnMouseDown()
        {
            if (!CanDrag()) return;
            StartDrag(Input.mousePosition);
        }

        private void Update()
        {
            if (isDragging && CanDrag())
            {
                Drag(Input.mousePosition);
                if (Input.GetMouseButtonUp(0))
                {
                    EndDrag(Input.mousePosition);
                }
            }
        }

        protected virtual void OnMouseUp()
        {
            if (!CanDrag()) return;
            EndDrag(Input.mousePosition);
        }

        bool IsPlaced()
        {
            Debug.Log("IsPlased");
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

        public void StartDrag(Vector3 inputScreenPos)
        {
            if (!CanDrag()) return;

            TransformCalculation.SetPositionXY(transform, inputScreenPos);
            offset = transform.position - inputScreenPos;
            isDragging = true;
            placed = false;
            if (inFrame)
            {
                transform.localScale = originalSize;
                inFrame = false;
                frameScr.PutOut();
                frameScr = null;
                //colli.isTrigger = false;
            }
            //backObject.SetActive(false);
            OrderInLayerChange(dragOrder);
            backSprite.transform.localScale *= 1.05f;
        }

        public void Drag(Vector3 inputScreenPos)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(inputScreenPos) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            if (IsPlaced())
            {
                outSprite.gameObject.SetActive(false);
            }
            else
            {
                outSprite.gameObject.SetActive(true);
            }
        }

        public void EndDrag(Vector3 inputScreenPos)
        {
            if (!CanDrag()) return;

            isDragging = false;

            if (FramePositionNow())
            {
                placed = false;
                otherPosition = transform.position;
            }
            else if (!ObjectRange.Instance.InRange(thisSprite) || IsOverLapping())
            {
                InitalPositionReset();
            }
            else
            {
                placed = true;
                otherPosition = transform.position;
            }
            //backObject.SetActive(true);
            OrderInLayerChange(placeOrder);
            backSprite.transform.localScale /= 1.05f;
            offset = Vector3.zero;
        }
        /// <returns>îÌÇ¡ÇƒÇ¢ÇÈ=>true</returns>
        private bool IsOverLapping()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = false; // Trigger Ç‡ä‹ÇﬂÇΩÇ¢èÍçáÇÕ true Ç…
            filter.SetLayerMask(Physics2D.DefaultRaycastLayers); // ëSÉåÉCÉÑÅ[ëŒè€Ç…Ç∑ÇÈ

            List<Collider2D> results = new List<Collider2D>();
            int count = colli.OverlapCollider(filter, results);

            return count > 0;
        }
        void OrderInLayerChange(int order)
        {
            thisSprite.sortingOrder = order;
            backSprite.LayerChange(order - 1);
            outSprite.LayerChange(order + 1);
        }

        Frame FindAvailableFrame()
        {
            Vector2 mouseWorldPos = transform.position;

            Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPos);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<Frame>(out var frame))
                {
                    if (frame.InObject == null)
                    {
                        return frame;
                    }
                }
            }
            return null;
        }

        bool FramePositionNow()
        {
            var frame = FindAvailableFrame();
            if (frame != null)
            {
                FrameSpriteIn(frame);
                return true;
            }
            return false;
        }

        bool IsFramePositionAvailable()
        {
            return FindAvailableFrame() != null;
        }


        public void FrameSpriteIn(Frame frame)
        {
            FitSpriteInSquare(frame.Sprite.bounds.size.x*0.85f);

            Vector3 pos = frame.transform.position;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);

            //colli.isTrigger = true;
            //outSprite.gameObject.SetActive(false);
            inFrame = true;
            frameScr = frame;
            frame.PutIn(gameObject);
        }


        void FitSpriteInSquare(float squareSize)
        {
            Vector2 input = thisSprite.bounds.size;

            float max = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
            if (max == 0) transform.localScale = Vector2.zero; // 0èúéZñhé~
            transform.localScale *=squareSize/ max;
        }





        protected bool CanDrag()
        {
            if (GameLoop.StageState != StageState.Setting) return false;
            return true;
        }

        void InitalPositionReset()
        {
            placed = true;
            transform.position = otherPosition;
        }

    }
}
