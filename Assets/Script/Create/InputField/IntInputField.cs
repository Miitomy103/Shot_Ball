using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    public class IntInputField : MonoBehaviour, ISetValue<int>
    {
        [SerializeField] TMP_InputField inputField;
        int value;

        private string key;
        private Dictionary<string, int> dict;
        public void SetValue(int v)
        {
            value = v;
            inputField.text = v.ToString();
        }

        public int GetValue()
        {
            return value;
        }

        private void Awake()
        {
            inputField.onValueChanged.AddListener(str =>
            {
                if (int.TryParse(str, out int result))
                    value = result;
            });
        }

        public void Setup(string key, Dictionary<string, int> data)
        {
            this.key = key;
            this.dict = data;
            inputField.onValueChanged.AddListener(OnChanged);
        }
        private void OnChanged(string str)
        {
            if (int.TryParse(str, out var v))
            {
                value = v;
                if (dict != null && key != null)
                    dict[key] = v;
            }
        }
    }

}
