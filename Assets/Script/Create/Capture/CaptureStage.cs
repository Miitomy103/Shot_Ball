using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create

{
    public class CaptureStage : MonoBehaviour
    {
        private const float AlphaCutoutThreshold = 0.01f;
        private const int RenderTextureDepthBits = 24;

        public Camera targetCamera;
        public SpriteRenderer back;
        public SpriteRenderer left;
        public SpriteRenderer right;

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
        }
        public void CaptureMasked(string filePath)
        {
            int width = Screen.width;
            int height = Screen.height;

            // --- ① Backをキャプチャ ---
            Texture2D backTex = CaptureSprite(targetCamera, back.gameObject, width, height);

            // --- ② Left/Rightをキャプチャ ---
            Texture2D leftTex = CaptureSprite(targetCamera, left.gameObject, width, height);
            Texture2D rightTex = CaptureSprite(targetCamera, right.gameObject, width, height);

            // --- ③ マスク処理（Left/Rightがある部分を透明にする） ---
            Color[] backPixels = backTex.GetPixels();
            Color[] leftPixels = leftTex.GetPixels();
            Color[] rightPixels = rightTex.GetPixels();

            for (int i = 0; i < backPixels.Length; i++)
            {
                if (leftPixels[i].a > AlphaCutoutThreshold || rightPixels[i].a > AlphaCutoutThreshold)
                {
                    // LeftかRightが描かれているところは透明化
                    backPixels[i] = new Color(0, 0, 0, 0);
                }
            }

            Texture2D resultTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            resultTex.SetPixels(backPixels);
            resultTex.Apply();

            // --- ④ 保存 ---
            byte[] bytes = resultTex.EncodeToPNG();
            System.IO.File.WriteAllBytes(filePath, bytes);
            Debug.Log("背景キャプチャ完了（Left/Right切り抜き済み）: " + filePath);
        }

        private Texture2D CaptureSprite(Camera cam, GameObject target, int width, int height)
        {
            RenderTexture rt = new RenderTexture(width, height, RenderTextureDepthBits);
            cam.targetTexture = rt;

            // 一時的に対象だけを描画
            int originalLayer = target.layer;
            int mask = LayerMask.NameToLayer("CaptureOnly");
            target.layer = mask;

            int originalMask = cam.cullingMask;
            cam.cullingMask = 1 << mask;

            cam.Render();

            RenderTexture.active = rt;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            // 復元
            target.layer = originalLayer;
            cam.cullingMask = originalMask;
            cam.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            return tex;
        }
    }

}
