using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    public class EditorDragObject : MonoBehaviour
    {
        void Start()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.sortingOrder = (int)OrderInLayer.NoDrag;
                return;
            }
        }
    }
}
