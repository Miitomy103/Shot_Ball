using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShotBall.Create
{
    /// <summary>
    /// ギミック名のテキストを表示し、親の幅に収まるよう自動でフォントサイズを縮小するクラス。
    /// </summary>
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

            // �ŏ��ɑ傫�߂͈̔͂�AutoSize�ݒ�
            tmp.fontSizeMax = 100;
            tmp.fontSizeMin = 1;

            tmp.ForceMeshUpdate(); // �X�V

            int count = 0;  
            // �t�H���g�T�C�Y��e���Ɏ��܂�܂ŉ�����
            while (tmp.preferredWidth > maxWidth && tmp.fontSize > tmp.fontSizeMin&& count<100)
            {
                tmp.fontSize -= 0.5f;
                tmp.ForceMeshUpdate();
                count++;
            }
        }
    }
}
