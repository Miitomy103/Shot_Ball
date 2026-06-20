using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class UIWindow : MonoBehaviour
    {
        StageState beforeState;
        float beforeTimeScale;
        private void OnEnable()
        {
            beforeState = GameLoop.StageState;
            beforeTimeScale = Time.timeScale;
            Time.timeScale = 0;
            GameLoop.Instance.ChangeState(StageState.Pose);
            Debug.Log("UIWindow OnEnable");
        }
        private void OnDisable()
        {
            GameLoop.Instance.ChangeState(beforeState);
            Time.timeScale = beforeTimeScale;
        }
    }
}
