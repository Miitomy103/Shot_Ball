using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    
    public class SpriteOrderChange : MonoBehaviour
    {
        [SerializeField] OrderInLayer OrderInLayer;
        // Start is called before the first frame update

        private void OnValidate()
        {



            ChangeLayer();
        }
        void Awake()
        {
            ChangeLayer();
        }
        void ChangeLayer()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.sortingOrder = (int)OrderInLayer;
                return;
            }

            TextMeshPro textMeshPro = GetComponent<TextMeshPro>();
            if (textMeshPro != null)
            {
                textMeshPro.sortingOrder = (int)OrderInLayer;
            }
        }
    }
}
