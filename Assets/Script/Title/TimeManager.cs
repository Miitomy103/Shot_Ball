using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Time.timeScaleを変更するクラス。
/// </summary>
public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// タイムスケールを指定した値に変更する。
    /// </summary>
    public void ScaleChange(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
