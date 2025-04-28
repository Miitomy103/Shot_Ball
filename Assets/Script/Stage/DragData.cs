using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class DragData : MonoBehaviour
    {
        public abstract int[,] GridData { get; }
    }
}
