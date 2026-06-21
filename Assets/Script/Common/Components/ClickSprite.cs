using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// スプライトがクリックされたときにイベントを発火する汎用クラス。
/// </summary>
public class ClickSprite : MonoBehaviour
{
    [SerializeField] UnityEvent OnClick;

    private void OnMouseDown()
    {
        OnClick.Invoke();
    }
}
