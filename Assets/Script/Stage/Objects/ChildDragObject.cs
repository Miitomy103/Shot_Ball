using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ChildDragObject : MonoBehaviour
    {
        ChildrenDragObject Parent;
        private void Start()
        {
            Parent = transform.parent.gameObject.GetComponent<ChildrenDragObject>();
        }
        private void OnMouseDown()
        {
            Vector3 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Parent.StartDrag(cameraPos);
        }
        private void OnMouseEnter()
        {
            Parent.OnMouseEnterExit(true);
        }
        private void OnMouseExit()
        {
            Parent.OnMouseEnterExit(false);
        }
    }
}
