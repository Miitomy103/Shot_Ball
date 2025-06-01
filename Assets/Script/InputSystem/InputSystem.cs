using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class InputSystem : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(1)) RightClick();
        }
        void RightClick()
        {
            
            Vector3 mousePosition = Input.mousePosition;
            ObjectRotation(mousePosition);
        }
        void ObjectRotation(Vector3 mousePosition)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent<RotationDragObject>(out var dragObject))
            {
                dragObject.IsClick();
            }
        }
    }
}
