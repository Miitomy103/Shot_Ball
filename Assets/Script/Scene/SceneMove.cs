using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    public class SceneMove : MonoBehaviour
    {
        public void MoveScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
