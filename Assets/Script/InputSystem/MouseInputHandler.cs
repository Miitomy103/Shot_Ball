using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// マウスの左右ボタンの状態と移動の有無を毎フレーム保持し、右クリックでオブジェクトの回転操作を行うクラス。
    /// </summary>
    public class MouseInputHandler : MonoBehaviour
    {
        static MouseInputHandler instance;
        public static MouseInputHandler Instance => instance;

        [SerializeField] bool r_Down,r_Button, r_Up;
        [SerializeField] bool l_Down,l_Button, l_Up;
        public bool RightDown => r_Down;
        public bool RightUp => r_Up;
        public bool LeftDown => l_Down;
        public bool LeftUp => l_Up;
        public bool RightButton => r_Button;
        public bool LeftButton => l_Button;
        [SerializeField] bool isMove;
        public bool IsMove=>isMove;
        Vector3 prevMousePosition; // 追加
        private void Awake()
        {
            instance = this;
        }
        void Update()
        {
            l_Down = Input.GetMouseButtonDown(0);
            l_Up = Input.GetMouseButtonUp(0);
            r_Down = Input.GetMouseButtonDown(1);
            r_Up = Input.GetMouseButtonUp(1);
            l_Button = Input.GetMouseButton(0);
            r_Button = Input.GetMouseButton(1);
            if (r_Down)RightClick();

            // マウス移動判定
            if (Input.mousePosition != prevMousePosition)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }
            prevMousePosition = Input.mousePosition;
        }
        void RightClick()
        {
            Vector3 mousePosition = Input.mousePosition;
            ObjectRotation(mousePosition);
        }
        void ObjectRotation(Vector3 mousePosition)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent<RotationDragObject>(out var dragObject))
            {
                dragObject.IsClick();
            }
        }
    }
}
