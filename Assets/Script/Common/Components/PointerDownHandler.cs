using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// ポインターが押されたときにイベントを発火する汎用クラス。
/// </summary>
public class PointerDownHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent onPointerDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke();
    }
    /// <summary>
    /// ポインター押下イベントにリスナーを登録する。
    /// </summary>
    public void AddListener(UnityAction action)
    {
        onPointerDown.AddListener(action);
    }
    /// <summary>
    /// ポインター押下イベントからリスナーを解除する。
    /// </summary>
    public void RemoveListener(UnityAction action)
    {
        onPointerDown.RemoveListener(action);
    }
}
