using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.Create
{
    public class FrameControll : MonoBehaviour
    {
        const int maxObject = 6;

        public List<SpriteRenderer> frames = new List<SpriteRenderer>();

        Camera cam;

        float baseSpacing => 0.32f * cam.orthographicSize;

        readonly float panelWidth = 3f;
        public void InFrame(CreateObject createObject)
        {

        }
        //public SpriteRenderer AddFrame()
        //{

        //}
        void PositionAdjustment()
        {
            if (cam == null) cam = Camera.main;

            float screenHeight = cam.orthographicSize * 2f;
            float screenWidth = screenHeight * cam.aspect;

            int frameCount = frames.Count;
            if (frameCount == 0) return;

            // spacingを高さから制限（オーバーフロー防止）
            float spacing = Mathf.Min(baseSpacing, screenHeight / (frameCount + 0.5f));

            // Y方向の中心オフセット計算
            float offset;
            if (frameCount % 2 == 0)
            {
                offset = (frameCount / 2f - 0.5f) * spacing;
            }
            else
            {
                offset = (frameCount / 2) * spacing;
            }

            // X座標：画面右端に揃える
            float x = cam.transform.position.x + screenWidth / 2f - panelWidth / 2f;

            for (int i = 0; i < frameCount; i++)
            {
                float y = (i * spacing) - offset;
                frames[i].transform.position = new Vector3(x, y, 0.5f);
            }
        }
    }
}
