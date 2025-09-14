using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class NewButton : MonoBehaviour,INullDisplay
    {
        [SerializeField] Texture2D texture;
        [SerializeField] Vector2 sizeDelta = new Vector2(150, 150);
        [SerializeField] Color color = Color.white;

        RawImage rawImage;
        private void Awake()
        {
            rawImage = GetComponentInChildren<RawImage>();
        }
        public void Display()
        {
            rawImage.texture = texture;
            rawImage.rectTransform.sizeDelta = sizeDelta;
            rawImage.color = color;
        }
    }
    public interface INullDisplay
    {
        void Display();
    }
}
