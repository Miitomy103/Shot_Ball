using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
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
            yield return null; // レイアウト更新を待つ
            Match();
        }

        public void Match()
        {
            float width = text.preferredWidth;

            // 左端位置はそのままで、右にだけ広げる
            spriteRect.sizeDelta = new Vector2(width + padding, spriteRect.sizeDelta.y);

            // 位置は変更しない（左端固定）
            // spriteRect.position = spriteRect.position; ← これは不要
        }
    }


}
