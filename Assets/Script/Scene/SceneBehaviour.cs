using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    public class SceneBehaviour : MonoBehaviour
    {
        public void NextScene()
        {
            SceneControl.NextScene(SceneManager.GetActiveScene().name);
            Debug.Log("NextScene");
        }
        public void TitleScene()
        {
            SceneControl.TitleScene();
        }
    }
}
