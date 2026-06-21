using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// ボタンの押下/解放状態に応じてイベントを発火するクラス。
/// ポインタが押されたままボタン外に出ても、再度ボタン内に入ると押下状態として扱う。
/// </summary>
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

    /// <summary>
    /// 押下状態を切り替え、状態に応じたイベントを発火する。
    /// </summary>
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
