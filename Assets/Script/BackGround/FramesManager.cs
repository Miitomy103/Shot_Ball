using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class FramesManager : MonoBehaviour
    {
        const float spacing = 1.6f;
        [SerializeField]GameObject flamePrefab;
        [SerializeField] DragObjects dragObjects;
        void Start()
        {
            GameObject[] dragObjs = dragObjects.GetObjects();
            int objectsCount = dragObjs.Length;
            float offset = 0;

            if(objectsCount%2==0)
            {
                offset = (objectsCount / 2f - 0.5f) * spacing;
            }
            else
            {
                offset = (objectsCount / 2) * spacing;
            }

            for (int i = 0; i < objectsCount; i++)
            {
                float y = (i * spacing) - offset;
                GameObject f= Instantiate(flamePrefab, new Vector3(7.5f, y, 0.5f), Quaternion.identity,transform);

                GameObject obj = dragObjs[i];
                if(obj.TryGetComponent<RuntimeDragObject>(out var dragObject))
                {
                    dragObject.FrameSpriteIn(f.GetComponent<Frame>());
                }
                else
                {
                    Debug.LogError("DragObject‚ª‚È‚¢");
                }
            }
        }
    }
}
