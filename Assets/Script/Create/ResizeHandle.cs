using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ShotBall.InGame;

namespace ShotBall.Create
{
    public class SpriteResizeHandle : MonoBehaviour, IDragHandler,IPointerUpHandler, IPointerDownHandler
    {
        public Transform target; // SpriteRendererを持つGameObject
        public Vector2 scaleDirection = Vector2.one; // 例: 左上なら (-1, 1)
        public float scaleSpeed = 0.01f;
        public RectTransform ThisRect { get;private set; }  

        private SpriteRenderer spriteRenderer;
        private CreateObject createObject;
        private Vector3 lastScale;

        public bool IsDragging => createObject != null && createObject.IsDragging; // ドラッグ中かどうか
        public bool IsResize => isResize; // リサイズ中かどうか
        [SerializeField] bool isResize;

        const float MIN_SCALE = 0.2f; // 最小スケール
        const float MAX_SCALE = 6f; // 最大スケール

        private void Awake()
        {
            ThisRect = GetComponent<RectTransform>();
            SetSprite(target.GetComponent<SpriteRenderer>());
        }
        public void SetSprite(SpriteRenderer s)
        {
            spriteRenderer = s;
            if (spriteRenderer != null)
            {
                lastScale = spriteRenderer.size;
            }
            createObject = spriteRenderer.GetComponent<CreateObject>();
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (spriteRenderer == null) return;

            Vector2 delta = eventData.delta;

            // 1. スケール更新
            Vector3 newScale = lastScale;
            newScale.x += delta.x * scaleDirection.x * scaleSpeed;
            newScale.y += delta.y * scaleDirection.y * scaleSpeed;

            // 制限
            newScale.x = Mathf.Clamp(newScale.x, MIN_SCALE, MAX_SCALE);
            newScale.y = Mathf.Clamp(newScale.y, MIN_SCALE, MAX_SCALE);

            // 2. 元サイズと新サイズの差分を取得
            Vector3 oldSize = Vector3.Scale(spriteRenderer.bounds.size, Vector3.one);
            spriteRenderer.size = newScale;
            Vector3 newSize = spriteRenderer.bounds.size;

            Vector3 sizeDiff = newSize - oldSize;

            // 3. 固定側が動かないように補正（位置を逆にずらす）
            Vector3 offset = new Vector3(
                sizeDiff.x * -0.5f * scaleDirection.x,
                sizeDiff.y * -0.5f * scaleDirection.y,
                0f
            );

            spriteRenderer.transform.position -= offset;

            lastScale = newScale;

            if(spriteRenderer.TryGetComponent<SpriteImage>(out var spriteImage))
            {
                spriteImage.SizeChange(newScale);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isResize = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isResize = true;
        }
    }
}
