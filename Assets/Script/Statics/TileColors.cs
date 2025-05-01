using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public enum TileColor
    {
        Original,
        Change,
    }

    [Serializable]
    public class TileColorEntry
    {
        public TileColor tileColor;
        public Color color;
    }

    [Serializable]
    public class TileColors
    {
        [SerializeField] private List<TileColorEntry> colorEntries = new List<TileColorEntry>(2);

        private Dictionary<TileColor, Color> colorMap;

        /// <summary>
        /// 使用前に手動で呼び出す必要あり（Dictionary 初期化）
        /// </summary>
        public void Initialize()
        {
            colorMap = new Dictionary<TileColor, Color>();
            foreach (var entry in colorEntries)
            {
                if (!colorMap.ContainsKey(entry.tileColor))
                    colorMap.Add(entry.tileColor, entry.color);
            }
        }

        public Color GetColor(TileColor tileColor)
        {
            if (colorMap != null && colorMap.TryGetValue(tileColor, out var color))
                return color;
            else
                return Color.white;
        }

        public void SetColor(TileColor tileColor, Color newColor)
        {
            foreach (var entry in colorEntries)
            {
                if (entry.tileColor == tileColor)
                {
                    entry.color = newColor;
                    if (colorMap != null)
                        colorMap[tileColor] = newColor;
                    return;
                }
            }

            // 該当なし → 新規追加
            var newEntry = new TileColorEntry { tileColor = tileColor, color = newColor };
            colorEntries.Add(newEntry);
            if (colorMap != null)
                colorMap.Add(tileColor, newColor);
        }
    }

}
