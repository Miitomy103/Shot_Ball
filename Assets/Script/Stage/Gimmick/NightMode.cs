using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 夜モードかどうかの状態を持つクラス。未実装(nightColorはSerializeField+staticかつsetterが無く機能していない)。
    /// </summary>
    public class NightMode : MonoBehaviour
    {
        static bool night = false;

        public static bool Night => night;

        /// <summary>
        /// 夜モードの状態を変更する。
        /// </summary>
        public void Change(bool mode)
        {
            night = mode;
        }

        [SerializeField]public static Color nightColor { get; }

    }
}
