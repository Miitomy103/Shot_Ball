using ShotBall.Audio;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// マウスでドラッグして配置できるオブジェクトの基底クラス。
    /// 配置可否の判定やフレームへの収納などの共通処理を持ち、表示差分は派生クラス(Derivation系メソッド)で実装する。
    /// </summary>
    public abstract class DragObject : ObjectBase
    {
        public BlockType BlockType { get; protected set; }
        protected bool IsDragging { get; set; }
        public bool Placed { get; set; }
        protected Vector3 offset { get;set; }
        protected Vector3 otherPosition;
        public Vector3 OriginalSize { get; private set; }

        public FrameData FrameData => new FrameData(OriginalSize, GetComponent<SpriteRenderer>().size, BlockType);

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
        /// <summary>
        /// 現在の位置に配置可能かどうか。
        /// </summary>
        public abstract bool IsPlaced();
        /// <summary>
        /// ドラッグを開始する。
        /// </summary>
        public void StartDrag(Vector3 inputScreenPos)
        {
            if (!CanDrag()||IsDragging) return;

            Vector3 inputWorldPos = Camera.main.ScreenToWorldPoint(inputScreenPos);
            inputWorldPos.z = transform.position.z;
            offset = transform.position - inputWorldPos;
            IsDragging = true;
            Placed = false;
            OrderInLayerChange();
            DragStartAction?.Invoke();
            StartDragDerivation(inputScreenPos);

            Tap.Instance.Play();
        }
        protected abstract void StartDragDerivation(Vector3 inputScreenPos);

        Vector3 mousePos;
        /// <summary>
        /// ドラッグ中、毎フレーム呼ばれて位置を更新する。
        /// </summary>
        public void Drag(Vector3 inputScreenPos)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(inputScreenPos) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            DragAction?.Invoke();
            DragDerivation(newPosition);
        }
        protected abstract void DragDerivation(Vector3 inputScreenPos);
        /// <summary>
        /// ドラッグを終了し、配置先(フレーム内/範囲内/元の位置に戻す)を確定する。
        /// </summary>
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
        /// <summary>
        /// 現在のマウス位置にFramesManagerがあるかどうかを判定する。
        /// </summary>
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
        /// <summary>
        /// スプライトをframeの大きさに収まるように拡大縮小・位置調整する。
        /// </summary>
        public abstract void FitSpriteInSquare(SpriteRenderer frame);
    }

}
