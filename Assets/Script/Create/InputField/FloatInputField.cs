using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    public class FloatInputField : MonoBehaviour, ISetValue<float>
    {
        [SerializeField] TMP_InputField inputField;

        private string key;
        private Dictionary<string, float> dict;
        private float value;

        public void Setup(string key, Dictionary<string, float> data)
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
                    dict[key] = v;
            }
        }
    }


}
