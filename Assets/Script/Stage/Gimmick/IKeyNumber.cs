using UnityEngine;

namespace ShotBall.InGame
{
    public interface IKeyNumber
    {
        int KeyNumber { get; }

        void ColorChange(Color color);
    }
}
