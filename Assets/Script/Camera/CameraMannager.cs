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
        float wallWidthInScreenPercent = 0.17f; // 壁が画面幅の5%に見える

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
        void AdjustWalls()
        {
            try
            {
                float height = cam.orthographicSize * 2f;
                float width = height * cam.aspect;

                // ワールド座標における見た目上の壁の幅（画面幅に対して何％か）
                float desiredWallWidth = width * wallWidthInScreenPercent;

                // 実際のスプライトサイズを取得（1単位がスプライト全体の幅・高さになる前提）
                Vector3 spriteSize = Vector3.one;

                // 壁のスケールを調整して「見た目の幅 = desiredWallWidth」「高さ = カメラの高さ」
                Vector3 wallScale = new Vector3(
                    desiredWallWidth / spriteSize.x,
                    height / spriteSize.y,
                    1f
                );

                if (leftWall != null)
                {
                    leftWall.localScale = wallScale;
                    leftWall.position = new Vector3(cam.transform.position.x - width / 2f + desiredWallWidth / 2f, cam.transform.position.y, 0f);
                }

                if (rightWall != null)
                {
                    rightWall.localScale = wallScale;
                    rightWall.position = new Vector3(cam.transform.position.x + width / 2f - desiredWallWidth / 2f, cam.transform.position.y, 0f);
                }

                float left = cam.transform.position.x - width / 2 + desiredWallWidth;
                float right = cam.transform.position.x + width / 2f - desiredWallWidth;

                Vector3 leftUp = new Vector3(left, height / 2);
                Vector3 rightDown = new Vector3(right, -(height / 2));

                ObjectRange.Instance.CameraSizeChange(leftUp, rightDown);
            }
            catch
            {

            }
        }
    }

}
