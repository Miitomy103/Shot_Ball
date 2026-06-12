using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEventTrigger : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent onTrueEvent;
    [SerializeField] UnityEvent onFalseEvent;
    bool isActive;
    bool isPressed;
    private void Update()
    {
        if(isPressed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isPressed = false;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SetActive(true);
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        SetActive(false);
        isPressed = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isPressed)
        {
            SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetActive(false);
    }

    void SetActive(bool onActive)
    {
        isActive = onActive;
        if (isActive)
        {
            onTrueEvent.Invoke();
        }
        else
        {
            onFalseEvent.Invoke();
        }
    }
}
