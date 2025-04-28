using System.Collections.Generic;
using System.IO;

public static class GridDataUtil
{
    // DictionaryƒNƒ‰ƒX‚ÌéŒ¾‚Æ‰Šú’l‚Ìİ’è
    static readonly Dictionary<char, int> Mapping = new Dictionary<char, int>()
    {
        {'0', 0},
        {'1', 1},
        {'2', 2},
        {'3', 3},
    };


    public static int[,] FromTemplate(string[] template)
    {
        int height = template.Length;
        int width = template[0].Length;
        int[,] result = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char c = template[y][x];
                result[y, x] = Mapping.ContainsKey(c) ? Mapping[c] : 0;
            }
        }

        return result;
    }
}
