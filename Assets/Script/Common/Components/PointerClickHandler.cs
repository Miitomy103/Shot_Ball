using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnityEvent onClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
