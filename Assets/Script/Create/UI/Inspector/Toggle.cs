using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// ボタンクリックでON/OFFを切り替えるトグルUIのクラス。
    /// </summary>
    public class Toggle : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] GameObject check;

        public bool IsCheck { get; private set; }

        /// <summary>
        /// チェック状態が変化したときに呼ばれる。
        /// </summary>
        public Action<bool> Checked { get; set; }

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            SetValue(!IsCheck);
        }
        /// <summary>
        /// チェック状態を設定し、見た目を更新してCheckedイベントを発火する。
        /// </summary>
        public void SetValue(bool value)
        {
            IsCheck = value;
            check.SetActive(value);
            Checked?.Invoke(value);
        }
    }
}
