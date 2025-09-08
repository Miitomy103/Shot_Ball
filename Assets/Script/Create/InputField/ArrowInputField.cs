using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ArrowInputField : MonoBehaviour,ISetValue<Vector2>
    {
        [SerializeField] ArrowToggle arrowToggle;
        private string key;
        private Action<string, Vector2, bool> dict;
        private Vector2 value;
        public void SetValue(Vector2 value)
        {
            this.value = value;
            arrowToggle.SetValue(value);
        }
        public Vector2 GetValue()
        {
            return arrowToggle.Direction;
        }
        public void OnValueChanged(Vector2 v)
        {
            value = v;
            if (dict != null && key != null)
            dict?.Invoke(key, v, true);
        }
        public void Setup(string key, Action<string, Vector2,bool> data)
        {
            this.key = key;
            this.dict = data;
            arrowToggle.Checked += OnValueChanged;
        }
    }
}
