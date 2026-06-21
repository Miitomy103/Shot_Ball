using TMPro;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// フレーム番号を表示するテキストに文字列を設定するクラス。
    /// </summary>
    public class FrameNumber : MonoBehaviour,IChangeText
    {
        [SerializeField] TextMeshPro text;
        /// <summary>
        /// 表示するテキストを変更する。
        /// </summary>
        public void ChangeText(string str)
        {
            text.text = str;
        }
    }
}
