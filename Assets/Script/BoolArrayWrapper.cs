using UnityEngine;
using System;

[Serializable]
public class BoolArrayWrapper
{
    public const int GridSize = 3;

    public bool[,] data;

    public BoolArrayWrapper()
    {
        // 3~3‚Ì‰Šú‰»
        data = new bool[GridSize, GridSize];
    }
}
