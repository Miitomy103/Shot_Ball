using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ClickSprite : MonoBehaviour
{
    [SerializeField] UnityEvent OnClick;

    private void OnMouseDown()
    {
        OnClick.Invoke();
    }
}
