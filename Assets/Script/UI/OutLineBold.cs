using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(Image))]
    public class OutLineBold : MonoBehaviour
    {
        private Outline[] outlines;
        private Image image;
        [SerializeField] Vector2 bold;

        private void OnValidate()
        {
            if (image == null) image = GetComponent<Image>();
            outlines = GetComponents<Outline>();

            foreach(var o in outlines)
            {
                o.effectDistance = bold / outlines.Length;
                o.effectColor = image.color;
            }
        }
    }
}
