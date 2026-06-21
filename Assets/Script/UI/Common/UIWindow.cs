using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 表示中はゲームを一時停止(Pose状態・タイムスケール0)にし、非表示になったら元の状態に戻すウィンドウ用クラス。
    /// </summary>
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
