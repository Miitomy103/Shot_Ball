using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class RuntimeDragObject : DragObject
    {
        bool isDragging = false;
        public bool placed;
        private Vector3 offset;

        private Vector3 otherPosition;

        bool inFrame = false;
        Frame frameScr;

        Vector3 originalSize;


        private void Start()
        {
            otherPosition = transform.position;
        }

        protected override void Awake()
        {
            originalSize = transform.localScale;
        }
        protected virtual void OnMouseDown()
        {
            if (!CanDrag()) return;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartDrag(Input.mousePosition);
        }

        private void Update()
        {
            if (isDragging&&CanDrag())
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


        public void StartDrag(Vector3 inputScreenPos)
        {
            if (!CanDrag()) return;

            isDragging = true;
            if (inFrame)
            {
                transform.localScale = originalSize;
                inFrame = false;
                frameScr.PutOut();
                frameScr = null;
            }
        }

        public void Drag(Vector3 inputScreenPos)
        {
            if (isDragging && CanDrag())
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(inputScreenPos) + offset;
                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            }
        }

        public void EndDrag(Vector3 inputScreenPos)
        {
            if (!CanDrag()) return;

            isDragging = false;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(inputScreenPos);

            if (FramePositionNow())
            {
                placed = false;
                otherPosition = transform.position;
            }
            else if(!ObjectRange.Instance.InRange(transform.position))
            {
                InitalPositionReset();
            }
            else
            {
                placed = true;
                otherPosition = transform.position;
            }
            offset = Vector3.zero;
        }


        bool FramePositionNow()
        {
            Vector2 mouseWorldPos = transform.position;

            Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPos);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent<Frame>(out var frame))
                {
                    if (frame.InObject == null)
                    {
                        FrameSpriteIn(frame);
                        return true;
                    }
                }
            }
            return false;
        }

        public void FrameSpriteIn(Frame frame)
        {
            SpriteRenderer frameSprite = frame.Sprite;

            FitSpriteInSquare(frameSprite.bounds.size.x * 0.75f);

            transform.position = frame.transform.position;

            inFrame = true;

            frameScr = frame;

            frame.PutIn(gameObject);
        }

        void FitSpriteInSquare(float squareSize)
        {
            Vector2 input = thisSprite.bounds.size;

            float max = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
            if (max == 0) transform.localScale = Vector2.zero; // 0èúéZñhé~
            transform.localScale /= max * squareSize;
        }



        protected bool CanDrag()
        {
            //if (GameLoop.StageState == StageState.Setting) return true;
            //return false;
            return true;
        }

        void InitalPositionReset()
        {
            transform.position = otherPosition;
        }

    }
}
