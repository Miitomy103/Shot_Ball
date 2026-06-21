using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// セーブデータの全削除を行うクラス。
    /// </summary>
    public class SaveBehaviour : MonoBehaviour
    {
        /// <summary>
        /// PlayerPrefsに保存された全データを削除する。
        /// </summary>
        public void AllReset()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
