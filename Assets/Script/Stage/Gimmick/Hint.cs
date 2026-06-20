using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class Hint : MonoBehaviour
    {
        public static bool IsHint { get; private set; }

        [SerializeField] CoinManager coinManager;

        [SerializeField] Image image;
        [SerializeField] Color activeColor;
        [SerializeField] Color inactiveColor;

        private void Start()
        {
            ChangeHint(false);
        }
        public void ChangeHint()
        {
            if (GameLoop.StageState != StageState.Setting) return;
            ChangeHint(!IsHint);
        }
        void ChangeHint(bool hint)
        {
            IsHint = hint;
            //coinManager.ActiveCoins(hint);
            image.color = (IsHint) ? activeColor : inactiveColor;
        }
    }
}
