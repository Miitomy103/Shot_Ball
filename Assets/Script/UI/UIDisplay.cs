using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall
{
    public class UIDisplay : MonoBehaviour
    {
        [SerializeField] private float displaySizeMagnification = 1;
        [SerializeField] private RectTransform nullObject;

        GameObject displayObject;

        public void NullDisplay()
        {
            nullObject.gameObject.SetActive(true);
            if (displayObject != null)
            {
                Destroy(displayObject);
                displayObject = null;
            }
        }
        public void GenerateDisplay(GameObject obj)
        {
            RectTransform r=Instantiate(obj.gameObject, transform).GetComponent<RectTransform>();
            SetDisplay(r);
        }
        public void SetDisplay(RectTransform rectTransform)
        {
            if (displayObject != null)
            {
                Destroy(displayObject);
            }
            nullObject.gameObject.SetActive(false);
            displayObject = rectTransform.gameObject;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * displaySizeMagnification, rectTransform.sizeDelta.y * displaySizeMagnification);
        }
    }
}
