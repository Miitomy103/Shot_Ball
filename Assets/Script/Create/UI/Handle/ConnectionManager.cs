using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// ブロック同士の接続(Connect)をUI上のハンドル・線で表示し、ドラッグで接続先を選択する処理を行うクラス。
    /// </summary>
    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance => instance;

        static ConnectionManager instance;

        [Header("PrefabUI")]
        [SerializeField] Image pointUI;
        [SerializeField] Image lineUI;

        [Header("アタッチ")]
        [SerializeField] Transform parent;
        [SerializeField] RectTransform centerPoint;
        [SerializeField] Canvas canvas;

        Connect connect;

        //Drag
        private RectTransform point;
        private RectTransform line;

        bool onMouseDown;

        public static bool isConnect;
        private void Awake()
        {
            instance = this;
            isConnect = false;
        }
        private void Start()
        {
            PointerDownHandler handler = centerPoint.GetComponent<PointerDownHandler>();
            if (handler == null) Debug.LogError("PointerDownHandlerがアタッチされていません");
            handler.AddListener(OnClick);
            CreateInputSystem.Instance.CameraScroll += Scroll;
            CreateInputSystem.Instance.CameraDrag += Scroll;
        }
        /// <summary>
        /// カメラのスクロール/移動に合わせて、表示中の接続ハンドルの位置を更新する。
        /// </summary>
        public void Scroll()
        {
            if(connect == null) return;
            centerPoint.position = Camera.main.WorldToScreenPoint(connect.transform.position);
        }
        /// <summary>
        /// 指定したConnectの接続UIを表示する。nullを渡すと非表示にする。
        /// </summary>
        public void Connection(Connect connect)
        {
            if (this.connect != null)
            {
                foreach (var c in this.connect.connectUIs)
                {
                    c.SetActive(false);
                }
            }
            if (connect == null)
            {
                centerPoint.gameObject.SetActive(false);
                return;
            }
            centerPoint.gameObject.SetActive(true);
            SetHandlePosition(centerPoint, connect.transform.position);



            this.connect = connect;
            foreach (var c in connect.connectUIs)
            {
                c.SetActive(true);
            }
        }
        void SetHandlePosition(RectTransform handle, Vector3 worldPos)
        {
            Camera worldCamera = Camera.main;
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(worldCamera, worldPos);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPoint,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : worldCamera,
                out Vector2 localPoint
            );

            handle.anchoredPosition = localPoint;
        }
        private void Update()
        {
            if(!onMouseDown) return;


            LineSet(Input.mousePosition,point,line);

            if (Input.GetMouseButtonUp(0))
            {
                onMouseDown = false;
                isConnect = false;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);



                foreach (var hit in hits)
                {
                    if (hit.collider.TryGetComponent<CreateObject>(out var createObject))
                    {
                        foreach (var c in connect.blockTypes)
                        {
                            if (c == createObject.StageBlockData.Type&&connect.connectUIs.Count<connect.limit)
                            {
                                foreach(var c2 in connect.connectUIs)
                                {
                                    if (c2.createObject == createObject)
                                    {
                                        Destroy(point.gameObject);
                                        Destroy(line.gameObject);
                                        point = null;
                                        line = null;
                                        return;
                                    }
                                }
                                LineSet(Camera.main.WorldToScreenPoint(createObject.transform.position), point, line);
                                ConnectUI connectUI = new ConnectUI(point, line);
                                connectUI.createObject = createObject;
                                connect.connectUIs.Add(connectUI);
                                point = null;
                                line = null;
                                return;
                            }
                        }
                    }
                }
                Destroy(point.gameObject);
                Destroy(line.gameObject);
                point = null;
                line = null;

            }

        }
        /// <summary>
        /// マウス位置をCanvas上のローカル座標に変換してpを移動し、線(l)を更新する。
        /// </summary>
        public void LineSet(Vector2 mousePos,RectTransform p,RectTransform l)
        {
            if(p == null || l == null || centerPoint == null || canvas == null) return;

            Debug.Log("LineSet"+mousePos);

            Vector2 localPoint;

            // スクリーン座標をCanvasのローカル座標に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                mousePos,
                canvas.worldCamera,
                out localPoint
            );

            // UIの位置を設定
            p.localPosition = localPoint;

            LineSet(p, l);
        }

        /// <summary>
        /// pとcenterPointの間を結ぶ線(l)の位置・長さ・向きを更新する。
        /// </summary>
        public void LineSet(RectTransform p,RectTransform l)
        {
            SetHandlePosition(centerPoint, connect.transform.position);

            Vector3 worldPos1 = p.position;
            Vector3 worldPos2 = centerPoint.position;

            // 線の位置
            Vector3 midPoint = (worldPos1 + worldPos2) / 2f;
            l.position = midPoint;

            // 長さ調整
            float length = Vector3.Distance(worldPos1, worldPos2);
            l.sizeDelta = new Vector2(l.sizeDelta.x, length); // 横方向に伸びる前提

            // 向き調整（+90度補正）
            Vector3 direction = (worldPos2 - worldPos1).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            l.rotation = Quaternion.Euler(0f, 0f, angle + 90f); // ← ここ！
        }

        void OnClick()
        {
            onMouseDown = true;
            isConnect = true;
            point=Instantiate(pointUI,parent).rectTransform;
            line = Instantiate(lineUI,parent).rectTransform;

            PointerDownHandler handler = point.GetComponent<PointerDownHandler>();
            if (handler == null) Debug.LogError("PointerDownHandlerがアタッチされていません");
            handler.AddListener(() => OnClickToPoint(new ConnectUI(point, line)));

            var connectUI = new ConnectUI(point, line);

        }
        void OnClickToPoint(ConnectUI connectUI)
        {
            if(connectUI.toPoint == null || connectUI.lineUI == null) Debug.LogError("ConnectUIの要素がnullです");    
            point = connectUI.toPoint;
            line = connectUI.lineUI;
            onMouseDown = true;
            isConnect = true;
            Debug.Log("OnClickToPoint");
        }
    }

}
