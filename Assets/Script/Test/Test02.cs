using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    /// <summary>
    /// デバッグ用の検証コード。ジェネリックで2つの値を保持するだけのクラス。
    /// </summary>
    public class Test02<T>
    {
        public T Speed;
        public T Smart;

        public Test02(T speed, T smart)
        {
            Speed = speed;
            Smart = smart;
        }
    }
}
