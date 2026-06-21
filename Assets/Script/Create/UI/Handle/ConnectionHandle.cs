using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    /// <summary>
    /// 2つのSpriteの間にUI上でハンドルと線を表示し、Spriteの位置に追従させるクラス。
    /// </summary>
    public class ConnectionHandle
    {
        private SpriteRenderer from;
        private SpriteRenderer to;
        private RectTransform handle;
        private Image line;
        private Canvas canvas;
        private Camera camera;

        public ConnectionHandle(SpriteRenderer from, SpriteRenderer to, RectTransform handle, Image line, Canvas canvas, Camera camera)
        {
            this.from = from;
            this.to = to;
            this.handle = handle;
            this.line = line;
            this.canvas = canvas;
            this.camera = camera;
        }

        /// <summary>
        /// ハンドルと線の位置を、現在のfrom/toの位置に合わせて更新する。
        /// </summary>
        public void Update()
        {
            if (from == null || to == null) return;

            // Spriteの中心ワールド座標
            Vector3 fromWorld = from.bounds.center;
            Vector3 toWorld = to.bounds.center;

            // Canvas上のローカル座標に変換
            Vector2 fromLocal = WorldToCanvasLocal(fromWorld);
            Vector2 toLocal = WorldToCanvasLocal(toWorld);

            // Handleを更新（Spriteの中心に置く）
            handle.anchoredPosition = fromLocal;

            // 線を更新
            UpdateLine(fromLocal, toLocal);
        }

        private Vector2 WorldToCanvasLocal(Vector3 worldPos)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, worldPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPoint,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : camera,
                out Vector2 localPoint
            );
            return localPoint;
        }

        private void UpdateLine(Vector2 start, Vector2 end)
        {
            Vector2 dir = end - start;
            float length = dir.magnitude;

            // 線の位置を2点の中間に置く
            line.rectTransform.anchoredPosition = (start + end) / 2f;

            // 線の長さを調整（横方向に伸びるImageを想定）
            line.rectTransform.sizeDelta = new Vector2(length, line.rectTransform.sizeDelta.y);

            // 回転を調整
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            line.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
