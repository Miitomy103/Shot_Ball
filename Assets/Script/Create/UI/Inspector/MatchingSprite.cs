using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace ShotBall.Create
{
    public class MatchingSprite : MonoBehaviour, IChangeText
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] RectTransform spriteRect;
        [SerializeField] float padding = 10f;

        public void ChangeText(string str)
        {
            if (text != null)
            {
                text.text = str;
                StartCoroutine(AdjustAfterFrame());
            }
        }

        private IEnumerator AdjustAfterFrame()
        {
            yield return null; // ���C�A�E�g�X�V��҂�
            Match();
        }

        public void Match()
        {
            float width = text.preferredWidth;

            // ���[�ʒu�͂��̂܂܂ŁA�E�ɂ����L����
            spriteRect.sizeDelta = new Vector2(width + padding, spriteRect.sizeDelta.y);

            // �ʒu�͕ύX���Ȃ��i���[�Œ�j
            // spriteRect.position = spriteRect.position; �� ����͕s�v
        }
    }


}
