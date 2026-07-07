using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollInput : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] float scrollSpeed = 2f;

    private void Awake()
    {
        if (scrollRect == null)
        {
            scrollRect = GetComponent<ScrollRect>();
        }
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            scrollRect.verticalNormalizedPosition += Input.mouseScrollDelta.y * 0.1f*scrollSpeed;
        }
    }
}
