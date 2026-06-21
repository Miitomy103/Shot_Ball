using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    /// <summary>
    /// 指定した名前のシーンへ遷移するクラス。
    /// </summary>
    public class SceneMove : MonoBehaviour
    {
        /// <summary>
        /// 指定したシーンへ遷移する。
        /// </summary>
        public void MoveScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
