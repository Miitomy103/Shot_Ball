using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// クリックされたときにイベントを発火する汎用クラス。
/// </summary>
public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnityEvent onClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
