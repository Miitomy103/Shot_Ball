using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Raycastの下にあるUIにイベントを転送するためのコンポーネント
/// </summary>
public class ForwardToBack : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject target; // 奥にあるScrollRectなど

    public void OnBeginDrag(PointerEventData eventData)
        => ExecuteEvents.Execute(target, eventData, ExecuteEvents.beginDragHandler);

    public void OnDrag(PointerEventData eventData)
        => ExecuteEvents.Execute(target, eventData, ExecuteEvents.dragHandler);

    public void OnEndDrag(PointerEventData eventData)
        => ExecuteEvents.Execute(target, eventData, ExecuteEvents.endDragHandler);

    public void OnPointerEnter(PointerEventData eventData)
        => ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerEnterHandler);

    public void OnPointerExit(PointerEventData eventData)
        => ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerExitHandler);
}