using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// テキストの表示幅に合わせて背景スプライト(spriteRect)の幅を一致させるクラス。左端を固定して右に伸縮する。
    /// </summary>
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

        /// <summary>
        /// テキストの表示幅にスプライトの幅を合わせる(左端固定)。
        /// </summary>
        public void Match()
        {
            float width = text.preferredWidth;

            // 左端位置はそのままで、右に伸び縮みする
            spriteRect.sizeDelta = new Vector2(width + padding, spriteRect.sizeDelta.y);

            // 位置は変更しない(左端固定)
            // spriteRect.position = spriteRect.position; ← これは不要
        }
    }


}
