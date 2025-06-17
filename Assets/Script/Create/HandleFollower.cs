using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    public class HandleFollower : MonoBehaviour
    {
        static HandleFollower instance;
        public static HandleFollower Instance => instance;

        public Camera worldCamera; // ’Êí‚ÍƒƒCƒ“ƒJƒƒ‰
        [SerializeField] SpriteResizeHandle topLeftHandle;
        [SerializeField] SpriteResizeHandle topRightHandle;
        [SerializeField] SpriteResizeHandle bottomLeftHandle;
        [SerializeField] SpriteResizeHandle bottomRightHandle;

        public SpriteRenderer spriteRenderer;
        private Canvas canvas;

        private SpriteResizeHandle[] Handles => new SpriteResizeHandle[]
        {
            topLeftHandle,
            topRightHandle,
            bottomLeftHandle,
            bottomRightHandle
        };
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            canvas = GetComponentInParent<Canvas>();

            if (worldCamera == null) worldCamera = Camera.main;

            ChangeHandles();
        }

        void Update()
        {
            if(InputSystem.Instance.IsDragging || InputSystem.Instance.IsZooming)
            {
                ChangeHandles();
                return;
            }
            foreach (var handle in Handles)
            {
                if (handle.IsResize||handle.IsDragging) ChangeHandles();
            }
        }



        public void SpriteSet(SpriteRenderer s,bool active)
        {
            spriteRenderer = s;
            foreach (var handle in Handles)
            {
                handle.SetSprite(s);
                handle.gameObject.SetActive(active);
            }
            ChangeHandles();
        }
        void ChangeHandles()
        {
            if (spriteRenderer == null) return;

            Transform t = spriteRenderer.transform;
            Vector2 size = spriteRenderer.bounds.size;

            Vector3[] corners = new Vector3[]
            {
        t.TransformPoint(new Vector3(-size.x / 2f,  size.y / 2f)), // Top Left
        t.TransformPoint(new Vector3( size.x / 2f,  size.y / 2f)), // Top Right
        t.TransformPoint(new Vector3(-size.x / 2f, -size.y / 2f)), // Bottom Left
        t.TransformPoint(new Vector3( size.x / 2f, -size.y / 2f)), // Bottom Right
            };

            SetHandlePosition(topLeftHandle.ThisRect, corners[0]);
            SetHandlePosition(topRightHandle.ThisRect, corners[1]);
            SetHandlePosition(bottomLeftHandle.ThisRect, corners[2]);
            SetHandlePosition(bottomRightHandle.ThisRect, corners[3]);
        }
        void SetHandlePosition(RectTransform handle, Vector3 worldPos)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(worldCamera, worldPos);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPoint,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : worldCamera,
                out Vector2 localPoint
            );

            handle.anchoredPosition = localPoint;
        }
    }

}
