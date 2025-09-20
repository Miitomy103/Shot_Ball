using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsDestroy : MonoBehaviour
{
    [SerializeField] UnityEvent onDestroyEvent;
    private void OnDestroy()
    {
        onDestroyEvent?.Invoke();
    }
}
