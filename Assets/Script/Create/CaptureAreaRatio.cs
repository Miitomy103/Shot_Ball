using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    using UnityEngine;

    [ExecuteAlways] // シーン編集中でも描画される
    public class CaptureAreaGizmo : MonoBehaviour
    {
        public Camera targetCamera;   // キャプチャに使うカメラ
        public float cameraSize = 5f; // OrthographicSize
        public Vector2 ratio = new Vector2(16, 9); // 画面比率（例：16:9）

        private void OnDrawGizmos()
        {
            if (targetCamera == null) return;

            // カメラの高さと幅を計算（Orthographic前提）
            float totalHeight = cameraSize * 2f;
            float totalWidth = totalHeight * (ratio.x / ratio.y);

            // カメラ位置を中心にワールド座標で矩形を描く
            Vector3 camPos = targetCamera.transform.position;
            Quaternion camRot = targetCamera.transform.rotation;

            // カメラが回転してても合わせる
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(camPos, camRot, Vector3.one);
            Gizmos.matrix = rotationMatrix;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.forward * targetCamera.nearClipPlane + Vector3.forward * 0.1f,
                                new Vector3(totalWidth, totalHeight, 0.01f));
        }
    }

}
