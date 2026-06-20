using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class PointerDownHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent onPointerDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke();
    }
    public void AddListener(UnityAction action)
    {
        onPointerDown.AddListener(action);
    }
    public void RemoveListener(UnityAction action)
    {
        onPointerDown.RemoveListener(action);
    }
}
