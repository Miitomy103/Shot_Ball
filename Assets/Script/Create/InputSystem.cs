using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    public class InputSystem : MonoBehaviour
    {
        static InputSystem instance;
        public static InputSystem Instance => instance;

        Camera cam => Camera.main;

        public bool IsDragging { get; private set; }
        [Header("ドラッグ設定")]
        Vector3 lastMousePosition;
        [SerializeField] float dragSpeed = 1f; // ドラッグ速度の調整

        public bool IsZooming { get; private set; }
        [Header("ズーム設定")]
        public float zoomSpeed = 10.0f;
        public float minZoomDistance = 5f;
        public float maxZoomDistance = 100f;

        public Action CameraScroll { get; set; }
        public Action CameraDrag { get; set; }
        private void Awake()
        {
            instance = this;
        }
        void Update()
        {
            Scroll();
            if (Input.GetMouseButtonDown(1)) // 右クリック開始
            {
                IsDragging = true;
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(1)) // 右クリック終了
            {
                IsDragging = false;
            }

            if (IsDragging)
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;

                float d=(cam.orthographicSize/7)*dragSpeed;

                float moveX = 1 * delta.x * d * Time.deltaTime;
                float moveY = 1 * delta.y * d * Time.deltaTime;

                // カメラを平行移動
                cam.transform.Translate(new Vector3(-moveX, -moveY, 0), Space.Self);

                lastMousePosition = Input.mousePosition;
                CameraDrag?.Invoke();
            }
        }
        void Scroll()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                IsZooming = true;
                // ズーム前のマウス位置（ワールド座標）
                Vector3 mouseWorldBefore = cam.ScreenToWorldPoint(Input.mousePosition);

                // orthographicSize を変更
                cam.orthographicSize -= scroll * zoomSpeed;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoomDistance, maxZoomDistance);

                // ズーム後のマウス位置（ワールド座標）
                Vector3 mouseWorldAfter = cam.ScreenToWorldPoint(Input.mousePosition);

                // カメラ位置を調整して、マウス位置が同じワールド座標になるようにする
                Vector3 offset = mouseWorldBefore - mouseWorldAfter;
                cam.transform.position += offset;
                CameraScroll?.Invoke();
            }
            else
            {
                IsZooming = false;
            }
        }
    }
}
