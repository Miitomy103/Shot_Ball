using UnityEngine;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// マウスポインターの侵入・退出をイベントとして通知する汎用クラス。
/// </summary>
public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action OnEnter { get; set; }
    public Action OnExit { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke();
    }
}
