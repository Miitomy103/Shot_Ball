using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class FramesManager : MonoBehaviour
    {
        [SerializeField] GameObject flamePrefab;
        [SerializeField] DragObjects dragObjects;
        float panelWidth = 3f;
        Camera cam;

        SpriteRenderer childSprite;

        void Start()
        {
            if (cam == null) cam = Camera.main;

            childSprite = GetComponentInChildren<SpriteRenderer>();

            float baseSpacing = 0.32f * cam.orthographicSize;

            float scale = 0.25f * cam.orthographicSize;

            GameObject[] dragObjs = dragObjects.GetObjects();
            int objectsCount = dragObjs.Length;

            float screenHeight = cam.orthographicSize * 2f;

            // spacingを高さから制限（オーバーフロー防止）
            float spacing = Mathf.Min(baseSpacing, screenHeight / (objectsCount + 0.5f));

            float offset;
            if (objectsCount % 2 == 0)
            {
                offset = (objectsCount / 2f - 0.5f) * spacing;
            }
            else
            {
                offset = (objectsCount / 2) * spacing;
            }

            // パネルの右端に合わせてx位置計算
            float screenWidth = screenHeight * cam.aspect;
            float x = cam.transform.position.x + screenWidth / 2f - panelWidth / 2f;

            for (int i = 0; i < objectsCount; i++)
            {
                float y = (i * spacing) - offset;
                GameObject f = Instantiate(flamePrefab, new Vector3(childSprite.transform.position.x, y, 0.5f), Quaternion.identity, transform);

                f.transform.localScale = new Vector3(scale, scale);

                GameObject obj = dragObjs[i];
                if (obj.TryGetComponent<RuntimeDragObject>(out var dragObject))
                {
                    dragObject.FrameSpriteIn(f.GetComponent<Frame>());
                }
                else
                {
                    Debug.LogError("DragObjectがない");
                }
            }
        }

    }
}
