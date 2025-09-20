using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void ScaleChange(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
