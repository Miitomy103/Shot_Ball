using System;
using TMPro;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// X/Y/Zの3つの入力欄でVector3パラメータを入力するインスペクターUIのクラス。
    /// </summary>
    public class Vector3InputField : MonoBehaviour, ISetValue<Vector3>
    {
        [SerializeField] TMP_InputField xField;
        [SerializeField] TMP_InputField yField;
        [SerializeField] TMP_InputField zField;
        Vector3 value;

        private string key;
        private Action<string, Vector3, bool> dict;

        public void SetValue(Vector3 v)
        {
            value = v;
            xField.text = v.x.ToString();
            yField.text = v.y.ToString();
            zField.text = v.z.ToString();
        }

        public Vector3 GetValue()
        {
            return value;
        }


        private void UpdateValue()
        {
            float x = float.TryParse(xField.text, out var vx) ? vx : 0f;
            float y = float.TryParse(yField.text, out var vy) ? vy : 0f;
            float z = float.TryParse(zField.text, out var vz) ? vz : 0f;
            value = new Vector3(x, y,z);
            if (dict != null && key != null)
            {
                dict?.Invoke(key, value, true);
            }
        }

        public void Setup(string key, Action<string, Vector3, bool> data)
        {
            this.key = key;
            this.dict = data;
            xField.onValueChanged.AddListener(_=>UpdateValue());
            yField.onValueChanged.AddListener(_=>UpdateValue());
            zField.onValueChanged.AddListener(_=>UpdateValue());
        }
    }

}
