using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    
    /// <summary>
    /// SpriteRendererまたはTextMeshProの描画順(sortingOrder)を、指定したOrderInLayerの値に合わせるクラス。
    /// </summary>
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
        /// <summary>
        /// アタッチされているSpriteRendererまたはTextMeshProの描画順を更新する。
        /// </summary>
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
