using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public static class TileColors
    {
        public static Dictionary<TileColor, Color> tileColors = new Dictionary<TileColor, Color>();

        public static void SetColor(Color original,Color change)
        {
            tileColors[TileColor.Original] = original;
            tileColors[TileColor.Change] = change;
        }

    }
    public enum TileColor
    {
        Original,
        Change,
    }
}
