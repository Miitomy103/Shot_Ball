
using TMPro;
using UnityEngine;
namespace ShotBall.InGame.UI
{

    /// <summary>
    /// テキストの内容が英語のみかどうかを判定し、適用するフォントを切り替えるクラス。
    /// </summary>
    public class FontSwapper : MonoBehaviour
    {
        public TMP_FontAsset englishFont;
        public TMP_FontAsset defaultFont;

        private TMP_Text text;

        void Start()
        {
            text = GetComponent<TMP_Text>();
            ApplyFont(text.text);
        }

        /// <summary>
        /// 文字列が英語のみで構成されているかを判定し、対応するフォントを適用する。
        /// </summary>
        void ApplyFont(string content)
        {
            // 英語のみの文字列なら英語フォント、それ以外ならデフォルト
            bool isEnglishOnly = System.Text.RegularExpressions.Regex.IsMatch(content, @"^[a-zA-Z0-9\s\p{P}]*$");
            text.font = isEnglishOnly ? englishFont : defaultFont;
        }
    }

}
