using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ChildDragObject : MonoBehaviour
    {
        ChindrenDragObject Parent;
        private void Start()
        {
            Parent = transform.parent.gameObject.GetComponent<ChindrenDragObject>();
        }
        private void OnMouseDown()
        {
            Vector3 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Parent.StartDrag(cameraPos);
        }
    }
}
