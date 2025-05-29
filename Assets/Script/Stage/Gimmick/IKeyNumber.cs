using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public interface IKeyNumber
    {
        int KeyNumber { get; set; }

        void ColorChange(Color color);
    }
}
