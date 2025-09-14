using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace ShotBall.Create
{
    public class HandleFollower : MonoBehaviour
    {
        static HandleFollower instance;
        public static HandleFollower Instance => instance;

        public Camera worldCamera; // 通常はメインカメラ
        [SerializeField] SpriteResizeHandle topLeftHandle;
        [SerializeField] SpriteResizeHandle topRightHandle;
        [SerializeField] SpriteResizeHandle bottomLeftHandle;
        [SerializeField] SpriteResizeHandle bottomRightHandle;

        [SerializeField] Lines lines;

        public SpriteRenderer spriteRenderer;
        private Canvas canvas;

        [SerializeField] HandleButton[] handleButtons;
        [SerializeField] Objects objects;

        private SpriteResizeHandle[] Handles => new SpriteResizeHandle[]
        {
            topLeftHandle,
            topRightHandle,
            bottomLeftHandle,
            bottomRightHandle
        };

        [SerializeField] ConnectionManager connectionManager;

        public Action OnResize { get; set; } // リサイズ開始時のアクション

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            canvas = GetComponentInParent<Canvas>();

            if (worldCamera == null) worldCamera = Camera.main;

            SpriteSet(null, false); // 初期状態ではハンドルを非表示にする

            ChangeHandles();
        }

        void Update()
        {
            if (InputSystem.Instance.IsDragging || InputSystem.Instance.IsZooming||isRotate)
            {
                ChangeHandles();
                return;
            }
            foreach (var handle in Handles)
            {
                if (handle.IsResize || handle.IsDragging) ChangeHandles();
            }
        }

        public bool ResizeNow()
        {
            foreach (var handle in Handles)
            {
                if (handle.IsResize ) return true;
            }
            return false;
        }

        public void SpriteSet(SpriteRenderer s, bool active)
        {
            if(s==null) active = false; // null の場合は非表示にする
            spriteRenderer = s;
            foreach (var handle in Handles)
            {
                handle.SetSprite(s);
                handle.gameObject.SetActive(active);
            }
            foreach (var button in handleButtons)
            {
                if(s==null) button.gameObject.SetActive(false);
                else  button.gameObject.SetActive(true);
            }
            if (lines == null||s==null)  lines.ActiveSelf(false);
            else lines.ActiveSelf(true);
            ChangeHandles();
            if (s == null) return;
            connectionManager.Connection(s.gameObject.GetComponent<Connect>());
        }
        void ChangeHandles()
        {
            if (spriteRenderer == null) return;

            Transform t = spriteRenderer.transform;

            // Sliced/Tiled の場合は spriteRenderer.size が「見た目のサイズ」
            Vector2 finalSize = spriteRenderer.size;

            // ローカル空間での四隅（中心基準）
            Vector3[] localCorners = new Vector3[]
            {
        new Vector3(-finalSize.x / 2f,  finalSize.y / 2f, 0), // Top Left
        new Vector3( finalSize.x / 2f,  finalSize.y / 2f, 0), // Top Right
        new Vector3(-finalSize.x / 2f, -finalSize.y / 2f, 0), // Bottom Left
        new Vector3( finalSize.x / 2f, -finalSize.y / 2f, 0), // Bottom Right
            };

            // ワールド座標に変換（回転はTransformが持ってるのでOK）
            Vector3[] worldCorners = new Vector3[4];
            for (int i = 0; i < 4; i++)
                worldCorners[i] = t.TransformPoint(localCorners[i]);

            // UIハンドルを更新
            SetHandlePosition(topLeftHandle.ThisRect, worldCorners[0]);
            SetHandlePosition(topRightHandle.ThisRect, worldCorners[1]);
            SetHandlePosition(bottomLeftHandle.ThisRect, worldCorners[2]);
            SetHandlePosition(bottomRightHandle.ThisRect, worldCorners[3]);

            OnResize?.Invoke();
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
        [SerializeField] private bool isRotate = false;
        private float rotationSpeed = 0.3f; // 好みに合わせて調整

        private Vector3 prevMousePos;
        public void RotateDown()
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            isRotate = true;
            prevMousePos = Input.mousePosition;
        }
        public void RotateUp()
        {
            isRotate = false;
        }
        public void RotateDrag()
        {
            Vector3 mouseDelta = Input.mousePosition - prevMousePos;
            float rotateAmount = mouseDelta.x * rotationSpeed;
            spriteRenderer.transform.Rotate(0, 0, -rotateAmount); // Z軸回転（右ドラッグで時計回り）

            prevMousePos = Input.mousePosition;
        }
        public void Delete()
        {
            Destroy(spriteRenderer.gameObject);
            SpriteSet(null, false);


            StartCoroutine(WaitForFrame());
            Debug.Log("Delete");
        }
        IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();
            objects.UpdateList();
        }

    }
}
