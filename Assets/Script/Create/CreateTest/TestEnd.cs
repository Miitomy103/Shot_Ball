using ShotBall.Create;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    public class TestEnd : MonoBehaviour
    {
        public void EndTest()
        {
            CreateManager.Instance.TestEnd();
        }
    }
}
