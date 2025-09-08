using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public interface ISetValue<T>
    {
        void Setup(string key, Action<string, T, bool> data);
        void SetValue(T value);
        T GetValue();
    }

}
