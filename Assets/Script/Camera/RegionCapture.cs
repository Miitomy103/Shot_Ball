using UnityEngine;
using System.Collections;
using System.IO;

namespace ShotBall.InGame
{
    public class RegionCapture : MonoBehaviour
    {
        // スクリーン座標（左下と右上の2点）
        public Vector2 screenPoint1 = new Vector2(100, 100);
        public Vector2 screenPoint2 = new Vector2(400, 300);

        public void Capture()
        {
            StartCoroutine(CaptureRoutine());
        }

        private IEnumerator CaptureRoutine()
        {
            yield return new WaitForEndOfFrame(); // UIを含めてレンダリングが完了するのを待つ

            Texture2D fullScreenshot = ScreenCapture.CaptureScreenshotAsTexture();

            Vector3 pointA = ObjectRange.pointA;
            Vector3 pointB = ObjectRange.pointB;

            Vector3 screenPoint1=Camera.main.WorldToScreenPoint(pointA);
            Vector3 screenPoint2 = Camera.main.WorldToScreenPoint(pointB);

            // 2点から矩形を作成（左下・幅・高さ）
            int xMin = Mathf.RoundToInt(Mathf.Min(screenPoint1.x, screenPoint2.x));
            int yMin = Mathf.RoundToInt(Mathf.Min(screenPoint1.y, screenPoint2.y));
            int width = Mathf.RoundToInt(Mathf.Abs(screenPoint2.x - screenPoint1.x));
            int height = Mathf.RoundToInt(Mathf.Abs(screenPoint2.y - screenPoint1.y));

            // 矩形範囲を切り取る
            Texture2D cropped = new Texture2D(width, height, TextureFormat.RGB24, false);
            cropped.SetPixels(fullScreenshot.GetPixels(xMin, yMin, width, height));
            cropped.Apply();

            // 保存
            byte[] pngData = cropped.EncodeToPNG();
            string path = Application.dataPath + $"/Picture/AreaScreenshot.{SceneControl.NowStage()}.png";
            File.WriteAllBytes(path, pngData);
            Debug.Log("Saved screenshot to: " + path);

            Destroy(fullScreenshot);
            Destroy(cropped);
        }
    }
}


