using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Collider2D上にマウスがホバーしているかを毎フレーム判定し、進入・退出時にイベントを発火するクラス。
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MouseEnter : MonoBehaviour
{
    /// <summary>
    /// いずれかのMouseEnter対象上にマウスがあるかどうか。
    /// </summary>
    public static bool inMouse;

    [SerializeField] UnityEvent onMouseEnter;
    [SerializeField] UnityEvent onMouseExit;

    [SerializeField, Tooltip("マウスを押している間のみ動作する")] bool isOnDownOnly = false;

    private bool isHover = false;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError("Collider2D が必要です");
        }
    }

    private void Update()
    {
        if (col == null) return;

        // カメラからマウス位置を取得
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // マウス位置にある全ての2D Colliderを取得
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

        bool hovering = false;
        foreach (var hit in hits)
        {
            if (hit.collider == col)
            {
                hovering = true;
                break;
            }
        }

        bool isActive = isOnDownOnly ? (hovering && Input.GetMouseButton(0)) : hovering;

        if (isActive && !isHover)
        {
            isHover = true;
            inMouse = true;
            onMouseEnter?.Invoke();
        }
        else if (!isActive && isHover)
        {
            isHover = false;
            inMouse = false;
            onMouseExit?.Invoke();
        }
    }
}
