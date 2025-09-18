using ShotBall.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class DragObject : ObjectBase
    {
        public BlockType BlockType { get; protected set; }
        protected bool IsDragging { get; set; }
        public bool Placed { get; set; }
        protected Vector3 offset { get;set; }
        protected Vector3 otherPosition;
        public Vector3 OriginalSize { get; private set; }

        public FrameData FrameData => new FrameData(OriginalSize, BlockType);

        protected const int placeOrder = 3;
        protected const int dragOrder = 7;
        public bool InFrame { get; set; }
        public Frame frameScript { get; set; }

        public Action DragStartAction { get; set; }
        public Action DragAction { get; set; }
        public Action DragEndAction { get; set; }
        protected virtual void Awake()
        {
            OriginalSize = transform.localScale;
        }
        protected virtual void Start() { }//TODO
        private void Update()
        {
            if (IsDragging && CanDrag())
            {
                Drag(Input.mousePosition);
                if (Input.GetMouseButtonUp(0))
                {
                    EndDrag(Input.mousePosition);
                }
            }
        }
        public abstract bool IsPlaced();
        public void StartDrag(Vector3 inputScreenPos)
        {
            if (!CanDrag()||IsDragging) return;

            TransformCalculation.SetPositionXY(transform, inputScreenPos);
            offset = transform.position - inputScreenPos;
            IsDragging = true;
            Placed = false;
            OrderInLayerChange();
            DragStartAction?.Invoke();
            StartDragDerivation(inputScreenPos);

            Tap.Instance.Play();
        }
        protected abstract void StartDragDerivation(Vector3 inputScreenPos);
        public void Drag(Vector3 inputScreenPos)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(inputScreenPos) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            DragAction?.Invoke();
            DragDerivation(newPosition);
        }
        protected abstract void DragDerivation(Vector3 inputScreenPos);
        public void EndDrag(Vector3 inputScreenPos)
        {
            if (!CanDrag()) return;

            IsDragging = false;

            if (FrameInMethod(out var frame))
            {
                frameScript.PutIn();
                Destroy(gameObject);
                Placed = false;
                otherPosition = transform.position;
            }
            else if (!InRange() || IsOverLapping())
            {
                InitalPositionReset();
            }
            else
            {
                InFrame = false;
                Placed = true;
                otherPosition = transform.position;
            }
            //backObject.SetActive(true);
            OrderInLayerChange();
            offset = Vector3.zero;
            DragEndAction?.Invoke();
            EndDragDerivation(inputScreenPos);
        }
        protected abstract void EndDragDerivation(Vector3 inputScreenPos);
        protected abstract bool CanDrag();
        protected void InitalPositionReset()
        {
            Debug.Log("InitalPositionReset()");
            Placed = true;
            transform.position = otherPosition;
            if (InFrame)
            {
                frameScript.PutIn();
                Destroy(gameObject);
            }
        }
        protected abstract bool InRange();
        public bool FrameInMethod(out FramesManager frame)
        {
            frame = null;

            // スクリーン座標 → ワールド座標に変換
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // クリック位置にあるすべてのCollider2Dを取得
            Collider2D[] hits = Physics2D.OverlapPointAll(worldPos);

            foreach (var hit in hits)
            {
                if (hit.isTrigger && hit.gameObject.TryGetComponent<FramesManager>(out var f))
                {
                    frame = f;
                    return true;
                }
            }

            return false;
        }
        protected bool IsFramePositionAvailable()
        {
            return FrameInMethod(out var frame);
        }
        protected abstract void OrderInLayerChange();
        public abstract void FitSpriteInSquare(SpriteRenderer frame);
    }

}
