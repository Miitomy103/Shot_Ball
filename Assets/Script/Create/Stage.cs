using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShotBall.Create
{
    public class Stage : MonoBehaviour
    {
        private const float DefaultCameraSize = 5f;
        private const float DefaultCenterWorldWidth = 11.8f;
        private const float AspectWidth = 17.77778f;
        private const float AspectHeight = 10f;
        private const int RenderTextureDepthBits = 24;

        [Header("背景")]
        [SerializeField] SpriteRenderer back;
        [SerializeField] float cameraSize = DefaultCameraSize;
        public float CameraSize => cameraSize;

        [SerializeField] float centerWorldWidth = DefaultCenterWorldWidth;
        public float CenterWorldWidth => centerWorldWidth;

        [SerializeField] SpriteRenderer left;
        [SerializeField] SpriteRenderer right;

        public float topY => back.bounds.max.y;
        public float bottonY => back.bounds.min.y;
        // 画面アスペクト比 (例: 16:9 → 1.777...)
        Vector2 ratio = new Vector2(AspectWidth, AspectHeight);

        private void OnValidate()
        {
            AdjustBackGround();
        }
        public void SizeSet(float cameraSize,float centerWorldWidth)
        {
            this.cameraSize = cameraSize;
            this.centerWorldWidth = centerWorldWidth;
            AdjustBackGround();
        }

        public void AdjustBackGround()
        {
            float totalHeight = cameraSize * 2f;
            float aspectRatio = ratio.x / ratio.y;
            float totalWidth = totalHeight * aspectRatio;

            // -------------------------------
            // back のサイズは ratio に基づいた比率で調整
            // -------------------------------
            float backSpriteWidth = back.sprite.bounds.size.x;
            float backSpriteHeight = back.sprite.bounds.size.y;

            // ratio に基づくスケール（縦サイズ基準）
            float backScaleY = totalHeight / backSpriteHeight;
            float backScaleX = (totalHeight * (ratio.x / ratio.y)) / backSpriteWidth;
            back.transform.localScale = new Vector3(backScaleX, backScaleY, 1f);
            back.transform.localPosition = Vector3.zero;
            float c=centerWorldWidth * (cameraSize / DefaultCameraSize); // centerWorldWidth を cameraSize に応じて拡大縮小

            // -------------------------------
            // left/right は centerWorldWidth を使って余り幅を埋めるように調整
            // -------------------------------
            float sideWidth = (totalWidth - c) / 2f;

            float sideSpriteWidth = left.sprite.bounds.size.x;
            float sideSpriteHeight = left.sprite.bounds.size.y;

            float sideScaleX = sideWidth / sideSpriteWidth;
            float sideScaleY = totalHeight / sideSpriteHeight;

            Vector3 sideScale = new Vector3(sideScaleX, sideScaleY, 1f);
            left.transform.localScale = sideScale;
            right.transform.localScale = sideScale;

            // 位置調整（左右対称）
            left.transform.localPosition = new Vector3(-c / 2f - sideWidth / 2f, 0f, 0f);
            right.transform.localPosition = new Vector3(c / 2f + sideWidth / 2f, 0f, 0f);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(-centerWorldWidth / 2f, topY, 0f), new Vector3(centerWorldWidth / 2f, topY, 0f));
            Gizmos.DrawLine(new Vector3(-centerWorldWidth / 2f, bottonY, 0f), new Vector3(centerWorldWidth / 2f, bottonY, 0f));
            Gizmos.DrawLine(new Vector3(-centerWorldWidth / 2f, topY, 0f), new Vector3(-centerWorldWidth / 2f, bottonY, 0f));
            Gizmos.DrawLine(new Vector3(centerWorldWidth / 2f, topY, 0f), new Vector3(centerWorldWidth / 2f, bottonY, 0f));
        }
        public Texture2D CaptureArea()
        {
            Camera targetCamera = Camera.main;

            // ① 矩形の4点をワールド座標で定義
            Vector3 worldBL = new Vector3(-centerWorldWidth / 2f, bottonY, 0f); // 左下
            Vector3 worldTR = new Vector3(centerWorldWidth / 2f, topY, 0f);     // 右上

            // ② スクリーン座標に変換
            Vector3 screenBL = targetCamera.WorldToScreenPoint(worldBL);
            Vector3 screenTR = targetCamera.WorldToScreenPoint(worldTR);

            // ③ Rectに変換
            int x = Mathf.RoundToInt(screenBL.x);
            int y = Mathf.RoundToInt(screenBL.y);
            int width = Mathf.RoundToInt(screenTR.x - screenBL.x);
            int height = Mathf.RoundToInt(screenTR.y - screenBL.y);

            // ④ RenderTextureにカメラを描画
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, RenderTextureDepthBits);
            targetCamera.targetTexture = rt;
            targetCamera.Render();

            // ⑤ 指定範囲をTexture2Dにコピー
            RenderTexture.active = rt;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(x, y, width, height), 0, 0);
            tex.Apply();

            // ⑥ 後片付け
            targetCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            // ⑦ Texture2Dを返す
            return tex;
        }

    }


}
