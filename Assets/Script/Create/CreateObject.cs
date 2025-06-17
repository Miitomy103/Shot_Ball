using ShotBall.InGame;
using UnityEngine;

namespace ShotBall.Create
{
    public class CreateObject : MonoBehaviour
    {
        [SerializeField] private bool isSizeChange = true;

        SpriteRenderer ThisSprite;

        public bool IsDragging { get;private set; } // ドラッグ中かどうか

        [SerializeField] string gimmickName;

        private void Awake()
        {
            ThisSprite = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            if (IsDragging)
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                worldPosition.z = 0; // Z軸の値を0に設定（2Dゲームの場合）
                transform.position = worldPosition;
                if (Input.GetMouseButtonUp(0)) // 左クリックを離したとき
                {
                    IsDragging = false;
                }
            }
        }
        private void OnMouseDown()
        {
            Debug.Log("CreateObject OnMouseDown");
            HandleFollower.Instance.SpriteSet(ThisSprite, isSizeChange);
            IsDragging = true;
            Inspector.Instance.Choice(gimmickName);
        }
    }
}
