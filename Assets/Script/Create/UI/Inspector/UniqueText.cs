using TMPro;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// 親の幅・高さに収まるようフォントサイズを自動調整するテキスト表示クラス。
    /// </summary>
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

            int maxLoop = 100; // 無限ループ防止
            while ((tmp.preferredWidth > maxWidth || tmp.preferredHeight > maxHeight) && tmp.fontSize > fontSizeMin && maxLoop-- > 0)
            {
                tmp.fontSize -= 1f;
                tmp.ForceMeshUpdate();
            }

            // 最小以下になるのを防ぐ
            if (tmp.fontSize < fontSizeMin)
                tmp.fontSize = fontSizeMin;
        }


    }
}
