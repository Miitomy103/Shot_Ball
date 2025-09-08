using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShotBall.InGame
{
    public class GimmickName : MonoBehaviour, IChangeText
    {
        [SerializeField] RectTransform parentRect;
        [SerializeField] TextMeshProUGUI tmp;
        [SerializeField] private float padding = 10f; // 左右の余白
        public void ChangeText(string str)
        {
            tmp.text = str;
            FitText();
        }
        void FitText()
        {
            if (tmp == null || parentRect == null) return;

            float maxWidth = parentRect.rect.width - padding * 2f;
            tmp.enableAutoSizing = true;
            tmp.enableWordWrapping = false;
            tmp.overflowMode = TextOverflowModes.Overflow;

            // 最初に大きめの範囲でAutoSize設定
            tmp.fontSizeMax = 100;
            tmp.fontSizeMin = 1;

            tmp.ForceMeshUpdate(); // 更新

            int count = 0;  
            // フォントサイズを親幅に収まるまで下げる
            while (tmp.preferredWidth > maxWidth && tmp.fontSize > tmp.fontSizeMin&& count<100)
            {
                tmp.fontSize -= 0.5f;
                tmp.ForceMeshUpdate();
                count++;
            }
        }
    }
}
