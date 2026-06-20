using System;

namespace ShotBall.Create
{
    public interface ISetValue<T>
    {
        void Setup(string key, Action<string, T, bool> data);
        void SetValue(T value);
        T GetValue();
    }

}
