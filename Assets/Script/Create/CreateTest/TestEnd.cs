using ShotBall.Create;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    /// <summary>
    /// テストプレイ用シーンの終了ボタンに付けるクラス。
    /// </summary>
    public class TestEnd : MonoBehaviour
    {
        /// <summary>
        /// テストプレイを終了し、CreateManagerに終了を通知する。
        /// </summary>
        public void EndTest()
        {
            CreateManager.Instance.TestEnd();
        }
    }
}
