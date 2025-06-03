using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShotBall.InGame
{
    public class FramesManager : MonoBehaviour
    {
        [SerializeField] GameObject flamePrefab;
        [SerializeField] DragObjects dragObjects;

        [SerializeField] SpriteRenderer frameRangeSprite;
        readonly float panelWidth = 3f;
        Camera cam;

        SpriteRenderer childSprite;

        Frame[] frames;

        void Start()
        {
            if (cam == null) cam = Camera.main;

            childSprite = GetComponentInChildren<SpriteRenderer>();

            float baseSpacing = 0.32f * cam.orthographicSize;

            float scale = 0.25f * cam.orthographicSize;

            DragObject[] dragObjs = dragObjects.GetObjects();
            // FrameData → Frame の対応表
            Dictionary<FrameData, Frame> frameDict = new Dictionary<FrameData, Frame>();

            int objCount = dragObjs.Length;
            HashSet<FrameData> uniqueFrames = new HashSet<FrameData>();
            foreach (var obj in dragObjs)
            {
                uniqueFrames.Add(obj.FrameData); // FrameData で Equals/GetHashCode が適切に実装されている必要あり
            }
            int frameCount = uniqueFrames.Count;

            float screenHeight = cam.orthographicSize * 2f;

            // spacingを高さから制限（オーバーフロー防止）
            float spacing = Mathf.Min(baseSpacing, screenHeight / (frameCount + 0.5f));

            float offset;
            if (frameCount % 2 == 0)
            {
                offset = (frameCount / 2f - 0.5f) * spacing;
            }
            else
            {
                offset = (frameCount / 2) * spacing;
            }

            // パネルの右端に合わせてx位置計算
            float screenWidth = screenHeight * cam.aspect;
            float x = cam.transform.position.x + screenWidth / 2f - panelWidth / 2f;

            frames = new Frame[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                float y = (i * spacing) - offset;
                GameObject f = Instantiate(flamePrefab, new Vector3(childSprite.transform.position.x, y, 0.5f), Quaternion.identity, transform);

                f.transform.localScale = new Vector3(scale, scale);

                Frame frame = f.GetComponent<Frame>();

                frames[i] = frame;
                FrameData frameData= uniqueFrames.ElementAt(i);
                frame.Data = frameData;
                foreach(var r in dragObjs)
                {
                    if (frameData.SameData(r.FrameData))
                    {
                        frame.Inialize(r);
                        break;
                    }
                }
            }
            foreach (var d in dragObjs)
            {
                InFrame(d);
            }
            BoxCollider2D colli = GetComponent<BoxCollider2D>();
            Vector2 size = new Vector2(scale * 1.2f, screenHeight*0.9f);
            if (colli!=null)
            {
                colli.offset = new Vector3(childSprite.transform.position.x, 0, 0.5f);
                colli.size = size;
            }
            if(colli!=null)
            {
                frameRangeSprite.drawMode = SpriteDrawMode.Sliced;
                frameRangeSprite.size = size;
                frameRangeSprite.gameObject.SetActive(false);
                frameRangeSprite.transform.localScale = Vector3.one;
                frameRangeSprite.transform.position = childSprite.transform.position;
            }
        }
        private void OnMouseEnter()
        {
            OnMouseEvent(true);
        }
        private void OnMouseExit()
        {
            OnMouseEvent(false);
        }
        private void OnMouseEvent(bool enter)
        {
            frameRangeSprite.gameObject.SetActive(enter);
        }
        private void OnMouseDown()
        {
            // ワールド座標に変換
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // クリック位置にあるすべてのCollider2Dを取得
            Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPos);

            foreach (Collider2D hit in hits)
            {
                if(hit.TryGetComponent<Frame>(out var frame))
                {
                    foreach(var f in frames)
                    {
                        if (f == frame) f.MouseDown();
                    }
                }
            }
        }
        public void InFrame(DragObject dragObject)
        {
            foreach(var f in frames)
            {
                if(f.IsFrame(dragObject.FrameData))
                {
                    f.PutIn();
                    Destroy(dragObject.gameObject);
                }
            }
        }
    }
}
