using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    /// <summary>
    /// 現在のシーンからシーン遷移操作（次のステージ・タイトルへ戻る）を呼び出すクラス。
    /// </summary>
    public class SceneBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 現在のシーン名をもとに次のステージへ遷移する。
        /// </summary>
        public void NextScene()
        {
            SceneControl.NextScene(SceneManager.GetActiveScene().name);
            Debug.Log("NextScene");
        }
        /// <summary>
        /// タイトルシーンへ遷移する。
        /// </summary>
        public void TitleScene()
        {
            SceneControl.TitleScene();
        }
    }
}
