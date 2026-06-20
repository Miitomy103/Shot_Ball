using System;
using TMPro;
using UnityEngine;

namespace ShotBall.Create
{
    public class Vector2InputField : MonoBehaviour, ISetValue<Vector2>
    {
        [SerializeField] TMP_InputField xField;
        [SerializeField] TMP_InputField yField;
        Vector2 value;

        private string key;
        private Action<string, Vector2, bool> dict;

        public void Setup(string key, Action<string, Vector2,bool> data)
        {
            this.key = key;
            this.dict = data;
            xField.onValueChanged.AddListener(_ => UpdateValue());
            yField.onValueChanged.AddListener(_ => UpdateValue());
        }
        public void SetValue(Vector2 v)
        {
            value = v;
            xField.text = v.x.ToString();
            yField.text = v.y.ToString();
        }

        public Vector2 GetValue()
        {
            return value;
        }

        private void Awake()
        {
            xField.onValueChanged.AddListener(_ => UpdateValue());
            yField.onValueChanged.AddListener(_ => UpdateValue());
        }

        private void UpdateValue()
        {
            float x = float.TryParse(xField.text, out var vx) ? vx : 0f;
            float y = float.TryParse(yField.text, out var vy) ? vy : 0f;
            value = new Vector2(x, y);
            if (dict != null && key != null)
            {
                dict?.Invoke(key, value, true);
            }
        }
    }

}
