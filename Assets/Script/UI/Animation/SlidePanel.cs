using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Okutani.UI
{
    public sealed class SlidePanel : MonoBehaviour
    {
        // パネルをどの方向からスライドさせるかを示すフラグ。
        public enum Side { Left, Right, Top, Bottom }

        [Header("Targets")]
        [SerializeField, Tooltip("操作対象の RectTransform。未設定ならこのコンポーネントの RectTransform を自動利用します。")]
        private RectTransform panel = null;
        [SerializeField, Tooltip("パネル表示中に入力を遮断するオーバーレイ。設定しない場合はブロッカー制御を行いません。")]
        private GameObject blocker = null;

        [Header("Behavior")]
        [SerializeField, Tooltip("パネルをどの方向からアニメーションさせるかを指定します。")]
        private Side side = Side.Left;
        [SerializeField, Tooltip("シーン開始時点でパネルを開いた状態にするかどうか。")]
        private bool startOpen = false;
        [SerializeField, Tooltip("true の場合は Time.timeScale の影響を受けない unscaled delta time で進みます。")]
        private bool useUnscaledTime = true;
        [SerializeField, Tooltip("開閉アニメーションにかける時間（秒）。")]
        private float duration = 0.30f;
        public float Duration => duration;
        [SerializeField, Tooltip("UIの外側を押して閉まる状態を指定します。0")]
        private bool closeOnOutsideClick = true;
        [SerializeField, Min(0f), Tooltip("閉じる際に確保する移動距離。0 の場合は RectTransform の幅/高さを採用します。")]
        private float slideDistance = 0f;
        [SerializeField, Tooltip("アンカー位置を補間するためのカーブ。EaseInOut など任意の曲線を設定できます。")]
        private AnimationCurve ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        // アニメーション状態を管理するための内部キャッシュ。
        private bool isOpen = false;
        public bool IsOpen => isOpen;
        private Coroutine animationCoroutine = null;
        private Vector2 openPosition;
        private Vector2 closedPosition;
        private readonly List<RaycastResult> pointerRaycastResults = new List<RaycastResult>();

        [Header("Action")]
        [SerializeField] UnityEvent onOpen;
        [SerializeField] UnityEvent onClose;

        private void Reset()
        {
            if (panel == null) panel = GetComponent<RectTransform>();
        }

        private void Awake()
        {
            if (panel == null) panel = GetComponent<RectTransform>();

            // 開いた状態のアンカー座標を記録しておき、ここを基準地点とする。
            openPosition = panel.anchoredPosition;

            // スライド距離は手動指定が優先、未指定なら Rect の幅/高さを採用する。
            float axisSize = (side == Side.Left || side == Side.Right)
                ? panel.rect.width
                : panel.rect.height;
            float travel = slideDistance > 0f ? slideDistance : axisSize;

            // 選択された方向に応じて閉じた位置のオフセットを求める。
            Vector2 offset = Vector2.zero;
            switch (side)
            {
                case Side.Left: offset = new Vector2(-travel, 0f); break;
                case Side.Right: offset = new Vector2(+travel, 0f); break;
                case Side.Top: offset = new Vector2(0f, +travel); break;
                case Side.Bottom: offset = new Vector2(0f, -travel); break;
            }
            closedPosition = openPosition + offset;

            // 初期状態とアンカー位置、ブロッカー表示を同期させておく。
            isOpen = startOpen;
            panel.anchoredPosition = isOpen ? openPosition : closedPosition;
            if (blocker != null) blocker.SetActive(isOpen);
        }

        private void Update()
        {
            // 開いているパネルの外側を押したら、通常の閉じる処理へ流す。
            if (!isOpen || panel == null)
            {
                return;
            }

            if (TryGetPointerDownPosition(out Vector2 pointerPosition) && IsPointerOutsidePanel(pointerPosition)&&closeOnOutsideClick)
            {
                Close();
            }
        }

        // OnClick などから呼び出し、開閉状態をトグルするヘルパー。
        public void Toggle()
        {
            if (isOpen) Close();
            else Open();
        }

        public void Open()
        {
            // 既存コルーチンを止めてから開くアニメーションを開始する。
            if (animationCoroutine != null) StopCoroutine(animationCoroutine);
            animationCoroutine = StartCoroutine(AnimateTo(openPosition));
            isOpen = true;
            if (blocker != null) blocker.SetActive(true);
        }

        public void Close()
        {
            // 進行中のアニメーションを打ち切って閉じる側へ補間する。
            if (animationCoroutine != null) StopCoroutine(animationCoroutine);
            animationCoroutine = StartCoroutine(AnimateTo(closedPosition));
            isOpen = false;
            if (blocker != null) blocker.SetActive(false);
        }

        private bool TryGetPointerDownPosition(out Vector2 pointerPosition)
        {
            // 押下開始だけを拾い、押しっぱなしで閉じ処理が連続しないようにする。
            if (Input.GetMouseButtonDown(0))
            {
                pointerPosition = Input.mousePosition;
                return true;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    pointerPosition = touch.position;
                    return true;
                }
            }

            pointerPosition = Vector2.zero;
            return false;
        }

        private bool IsPointerOutsidePanel(Vector2 pointerPosition)
        {
            // パネル配下のタブ、ボタン、確認ダイアログへの操作は外側クリック扱いにしない。
            if (IsPointerOnPanelUi(pointerPosition))
            {
                return false;
            }

            // 開閉ボタンなどパネル外の操作UIは、Button側の処理に任せる。
            if (IsPointerOnExternalSelectable(pointerPosition))
            {
                return false;
            }

            Camera eventCamera = ResolveEventCamera();
            return !RectTransformUtility.RectangleContainsScreenPoint(panel, pointerPosition, eventCamera);
        }

        private bool IsPointerOnPanelUi(Vector2 pointerPosition)
        {
            if (EventSystem.current == null)
            {
                return false;
            }

            pointerRaycastResults.Clear();
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = pointerPosition
            };
            EventSystem.current.RaycastAll(pointerEventData, pointerRaycastResults);

            foreach (RaycastResult result in pointerRaycastResults)
            {
                if (result.gameObject != null && result.gameObject.transform.IsChildOf(panel))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsPointerOnExternalSelectable(Vector2 pointerPosition)
        {
            if (EventSystem.current == null)
            {
                return false;
            }

            pointerRaycastResults.Clear();
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = pointerPosition
            };
            EventSystem.current.RaycastAll(pointerEventData, pointerRaycastResults);

            foreach (RaycastResult result in pointerRaycastResults)
            {
                if (result.gameObject == null || result.gameObject.transform.IsChildOf(panel))
                {
                    continue;
                }

                if (result.gameObject.GetComponentInParent<Selectable>() != null)
                {
                    return true;
                }
            }

            return false;
        }

        private Camera ResolveEventCamera()
        {
            // Overlay Canvas は null、Camera / World Canvas は設定済みカメラを使う。
            Canvas parentCanvas = GetComponentInParent<Canvas>();
            if (parentCanvas == null || parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return null;
            }

            return parentCanvas.worldCamera;
        }

        // 目標位置まで補間しながらアンカー座標を更新する共通コルーチン。
        private System.Collections.IEnumerator AnimateTo(Vector2 target)
        {
            Vector2 start = panel.anchoredPosition;
            float t = 0f;
            float dur = Mathf.Max(0.0001f, duration);

            while (t < 1f)
            {
                float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                t += dt / dur;
                float k = ease.Evaluate(Mathf.Clamp01(t));
                panel.anchoredPosition = Vector2.LerpUnclamped(start, target, k);
                yield return null;
            }

            // 最終フレームで目標座標にスナップし、コルーチン参照を解放する。
            panel.anchoredPosition = target;
            animationCoroutine = null;
            if (isOpen)
            {
                onOpen?.Invoke();
            }
            else
            {
                onClose?.Invoke();
            }
        }
    }
}
