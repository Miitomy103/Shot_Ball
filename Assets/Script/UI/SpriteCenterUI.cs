using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class SpriteCenterUI : MonoBehaviour
    {
        [SerializeField] Transform targetTransform;
        

        private void Start()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            RectTransform uiElement = GetComponent<RectTransform>();

            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTransform.position);

            // スクリーン座標 → UI座標
            Vector2 uiPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPos,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
                out uiPos
            );

            uiElement.anchoredPosition = new Vector2( uiPos.x,uiElement.anchoredPosition.y);
            //Debug.Log("World Position: " + targetTransform.position);
            //Debug.Log("Screen Position: " + screenPos);
            //Debug.Log("UI Position: " + uiElement.anchoredPosition);

        }
    }
}
