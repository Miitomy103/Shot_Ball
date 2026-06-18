using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// キーナンバーを持つオブジェクトが実装するインターフェース。
    /// OnOffBlock などと連動するギミックに使用する。
    /// </summary>
    public interface IKeyNumber
    {
        /// <summary>
        /// このオブジェクトのキーナンバー。
        /// </summary>
        int KeyNumber { get; }

        /// <summary>
        /// キーナンバーに対応する色に変更する。
        /// </summary>
        void ColorChange(Color color);
    }
}
