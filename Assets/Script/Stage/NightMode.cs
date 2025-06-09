using UnityEngine;

namespace ShotBall.InGame
{
    public class NightMode : MonoBehaviour
    {
        static bool night = false;

        public static bool Night => night;

        public void Change(bool mode)
        {
            night = mode;
        }

        [SerializeField]public static Color nightColor { get; }

    }
}
