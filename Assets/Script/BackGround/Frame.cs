using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Frame : MonoBehaviour
    {
        public GameObject InObject { get;private set; }

        public SpriteRenderer Sprite => GetComponent<SpriteRenderer>();

        private void OnMouseDown()
        {
            if(InObject!=null&&InObject.TryGetComponent<RuntimeDragObject>(out var dragObject))
            {
                dragObject.StartDrag(Camera.main.WorldToScreenPoint(Input.mousePosition));
            }
        }

        public void PutIn(GameObject obj)
        {
            InObject = obj;
        }

        public void PutOut()
        {
            InObject = null;
        }
    }
}
