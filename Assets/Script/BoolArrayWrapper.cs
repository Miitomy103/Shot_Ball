using UnityEngine;
using System;

[Serializable]
public class BoolArrayWrapper
{
    public bool[,] data;

    public BoolArrayWrapper()
    {
        // 3~3‚Ì‰Šú‰»
        data = new bool[3,3];
    }
}
