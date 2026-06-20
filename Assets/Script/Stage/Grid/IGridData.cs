using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public interface IGridData
    {
        int[,] GridData { get; }
        bool Center { get; }
    }
}
