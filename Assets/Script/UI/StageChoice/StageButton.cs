using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージ選択画面のステージボタン。所属するステージ番号とインデックスを保持する。
    /// </summary>
    public class StageButton : MonoBehaviour
    {
        public int Stage { get; set; }
        public int Index { get; set; }

    }
}
