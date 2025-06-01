using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class DragObject : MonoBehaviour
    {
        public string Name { get; protected set; }
        protected bool IsDragging { get; set; }
        public bool Placed { get; set; }
        protected Vector3 offset { get;set; }
        protected Vector3 otherPosition;
        public Vector3 OriginalSize { get; private set; }

        public FrameData FrameData => new FrameData(OriginalSize, Name);

        protected const int placeOrder = 3;
        protected const int dragOrder = 7;
        public bool inFrame { get; set; }
        public Frame frameScript { get; set; }
        protected virtual void Awake()
        {
            OriginalSize = transform.localScale;
        }
        protected virtual void Start() { inFrame = false; }//TODO
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
            if (!CanDrag()) return;

            TransformCalculation.SetPositionXY(transform, inputScreenPos);
            offset = transform.position - inputScreenPos;
            IsDragging = true;
            Placed = false;
            OrderInLayerChange(dragOrder);
            StartDragDerivation(inputScreenPos);
        }
        protected abstract void StartDragDerivation(Vector3 inputScreenPos);
        public void Drag(Vector3 inputScreenPos)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(inputScreenPos) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
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
                inFrame = false;
                Placed = true;
                otherPosition = transform.position;
            }
            //backObject.SetActive(true);
            OrderInLayerChange(placeOrder);
            offset = Vector3.zero;
            EndDragDerivation(inputScreenPos);
        }
        protected abstract void EndDragDerivation(Vector3 inputScreenPos);
        protected abstract bool IsOverLapping();
        protected abstract bool CanDrag();
        protected void InitalPositionReset()
        {
            Debug.Log("InitalPositionReset()");
            Placed = true;
            transform.position = otherPosition;
            if (inFrame)
            {
                frameScript.PutIn();
                Destroy(this);
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
                    Debug.Log("Frameあり");
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
        protected abstract void OrderInLayerChange(int o);
        public abstract void FitSpriteInSquare(SpriteRenderer frame);
    }

}
