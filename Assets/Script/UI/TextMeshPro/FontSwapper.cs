
using TMPro;
using UnityEngine;
namespace ShotBall.InGame.UI
{

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

        void ApplyFont(string content)
        {
            // 英語のみの文字列なら英語フォント、それ以外ならデフォルト
            bool isEnglishOnly = System.Text.RegularExpressions.Regex.IsMatch(content, @"^[a-zA-Z0-9\s\p{P}]*$");
            text.font = isEnglishOnly ? englishFont : defaultFont;
        }
    }

}
