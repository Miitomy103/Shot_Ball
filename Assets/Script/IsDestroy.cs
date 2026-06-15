using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// オブジェクトが破壊されたときにイベントを発火させるクラス
/// </summary>
public class IsDestroy : MonoBehaviour
{
    [SerializeField] UnityEvent onDestroyEvent;
    private void OnDestroy()
    {
        onDestroyEvent?.Invoke();
    }
}
