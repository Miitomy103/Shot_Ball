using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CameraManager : MonoBehaviour
    {
        public Camera cam;
        public Transform leftWall;
        public Transform rightWall;

        [SerializeField] float centerWorldWidth = 6f; // 中央のスペースをワールド単位で指定
        public float CenterWorldWidth { get => centerWorldWidth; set=> centerWorldWidth = value; }

        private void Awake()
        {
            if (cam == null) cam = Camera.main;
            AdjustWalls();
        }

        private void OnValidate()
        {
            if (cam == null) cam = Camera.main;
            AdjustWalls();
        }

        public void OnGUIButton()
        {
            if (cam == null) cam = Camera.main;
            AdjustWalls();
        }
        public void Culcuration()
        {
            float f = cam.orthographicSize / 5;
            Debug.Log(centerWorldWidth = 11.8f * f);
        }

        void AdjustWalls()
        {
            try
            {
                float height = cam.orthographicSize * 2f;
                float width = height * cam.aspect;

                // 壁の幅をワールド単位で計算
                float wallWidth = (width - centerWorldWidth) / 2f;
                if (wallWidth < 0f) wallWidth = 0f; // 負の幅を防ぐ

                // スプライト1単位サイズ前提（必要に応じてSpriteRendererから取得してもOK）
                Vector3 spriteSize = Vector3.one;

                Vector3 wallScale = new Vector3(
                    wallWidth / spriteSize.x,
                    height / spriteSize.y,
                    1f
                );

                if (leftWall != null)
                {
                    leftWall.localScale = wallScale;
                    leftWall.position = new Vector3(cam.transform.position.x - width / 2f + wallWidth / 2f, cam.transform.position.y, 0f);
                }

                if (rightWall != null)
                {
                    rightWall.localScale = wallScale;
                    rightWall.position = new Vector3(cam.transform.position.x + width / 2f - wallWidth / 2f, cam.transform.position.y, 0f);
                }

                // 中央の範囲（左右の壁の内側）を通知
                float left = cam.transform.position.x - width / 2f + wallWidth;
                float right = cam.transform.position.x + width / 2f - wallWidth;

                Vector3 leftUp = new Vector3(left, height / 2f);
                Vector3 rightDown = new Vector3(right, -height / 2f);

                ObjectRange.Instance.CameraSizeChange(leftUp, rightDown);
            }
            catch
            {
                Debug.LogWarning("AdjustWalls failed. Check references and sprite settings.");
            }
        }
    }
}
