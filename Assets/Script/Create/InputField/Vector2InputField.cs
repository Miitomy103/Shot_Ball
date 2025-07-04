using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class Vector2InputField : MonoBehaviour, ISetValue<Vector2>
    {
        [SerializeField] TMP_InputField xField;
        [SerializeField] TMP_InputField yField;
        Vector2 value;

        private string key;
        private Dictionary<string, Vector2> dict;

        public void Setup(string key, Dictionary<string, Vector2> data)
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
                dict[key] = value;
            }
        }
    }

}
