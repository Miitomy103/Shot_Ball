using System;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// トグルでboolパラメータを入力するインスペクターUIのクラス。
    /// </summary>
    public class BoolInputField : MonoBehaviour,ISetValue<bool>
    {
        [SerializeField] Toggle toggle;

        private string key;
        private Action<string, bool, bool> dict;
        private bool value;

        public void SetValue(bool value)
        {
            this.value = value;
            toggle.SetValue(value);
        }
        public bool GetValue()
        {
            return toggle.IsCheck;
        }
        public void OnValueChanged(bool v)
        {
            value = v;
            if (dict != null && key != null)
                dict?.Invoke(key, v, true);
        }

        public void Setup(string key, Action<string, bool, bool> data)
        {
            this.key = key;
            this.dict = data;
            toggle.Checked += OnValueChanged;
        }
    }
}
