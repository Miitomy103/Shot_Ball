using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace ShotBall.InGame
{
    public class UniqueText : MonoBehaviour,IChangeText
    {
        [SerializeField] RectTransform parentRect;
        [SerializeField] TextMeshProUGUI tmp;
        [SerializeField] private float paddingWidth = 10f;
        [SerializeField] private float paddingHeight = 10f;
        [SerializeField] private float fontSizeMin = 8f;
        [SerializeField] private float fontSizeMax = 100f;
        void Start()
        {
        
        }
        public void ChangeText(string str)
        {
            if (tmp == null || parentRect == null) return;
            tmp.text = str;
            FitText();
        }
        // Update is called once per frame
        void FitText()
        {
            if (tmp == null || parentRect == null) return;

            float maxWidth = parentRect.rect.width - paddingWidth * 2f;
            float maxHeight = parentRect.rect.height - paddingHeight * 2f;

            tmp.enableAutoSizing = false;
            tmp.enableWordWrapping = true;
            tmp.overflowMode = TextOverflowModes.Overflow;

            tmp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
            tmp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);

            tmp.fontSize = fontSizeMax;
            tmp.ForceMeshUpdate();

            int maxLoop = 100; // ñ≥å¿ÉãÅ[Évñhé~
            while ((tmp.preferredWidth > maxWidth || tmp.preferredHeight > maxHeight) && tmp.fontSize > fontSizeMin && maxLoop-- > 0)
            {
                tmp.fontSize -= 1f;
                tmp.ForceMeshUpdate();
            }

            // ç≈è¨à»â∫Ç…Ç»ÇÈÇÃÇñhÇÆ
            if (tmp.fontSize < fontSizeMin)
                tmp.fontSize = fontSizeMin;
        }


    }
}
