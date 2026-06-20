using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class Toggle : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] GameObject check;

        public bool IsCheck { get; private set; }

        public Action<bool> Checked { get; set; }

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            SetValue(!IsCheck);
        }
        public void SetValue(bool value)
        {
            IsCheck = value;
            check.SetActive(value);
            Checked?.Invoke(value);
        }
    }
}
