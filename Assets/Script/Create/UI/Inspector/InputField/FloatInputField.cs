using System;
using TMPro;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// 入力欄でfloatパラメータを入力するインスペクターUIのクラス。
    /// </summary>
    public class FloatInputField : MonoBehaviour, ISetValue<float>
    {
        [SerializeField] TMP_InputField inputField;

        private string key;
        private Action<string, float, bool> dict;
        private float value;

        public void Setup(string key, Action<string, float,bool> data)
        {
            this.key = key;
            this.dict = data;
            inputField.onValueChanged.AddListener(OnChanged);
        }

        public void SetValue(float v)
        {
            value = v;
            inputField.text = v.ToString();
        }

        public float GetValue() => value;

        private void OnChanged(string str)
        {
            if (float.TryParse(str, out var v))
            {
                value = v;
                if (dict != null && key != null)
                    dict?.Invoke(key, v, true);
            }
        }
    }


}
