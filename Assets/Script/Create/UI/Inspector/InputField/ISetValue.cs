using System;

namespace ShotBall.Create
{
    /// <summary>
    /// インスペクターの入力欄が値の取得・設定・変更通知を行うためのインターフェース。
    /// </summary>
    public interface ISetValue<T>
    {
        /// <summary>
        /// キーと、値が変更されたときに呼ぶコールバックを登録する。
        /// </summary>
        void Setup(string key, Action<string, T, bool> data);
        /// <summary>
        /// 値を設定し、UI表示にも反映する。
        /// </summary>
        void SetValue(T value);
        /// <summary>
        /// 現在の値を取得する。
        /// </summary>
        T GetValue();
    }

}
