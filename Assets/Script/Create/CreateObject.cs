using ShotBall.InGame;
using UnityEngine;

namespace ShotBall.Create
{
    public class CreateObject : MonoBehaviour
    {
        [SerializeField] private bool isSizeChange = true;

        SpriteRenderer ThisSprite;

        Vector3 offSet; // 修正: Vector2 から Vector3 に変更  

        public bool IsDragging { get; private set; } // ドラッグ中かどうか  

        [SerializeField,ReadOnly] string gimmickName;
        StageBlockData stageBlockData; // 追加: StageBlockDataの参照

        private void Awake()
        {
            ThisSprite = GetComponent<SpriteRenderer>();
            gimmickName=GetComponent<IBlockData>().Type.ToString();
            stageBlockData = GetComponent<StageBlockData>();
        }
        private void Update()
        {
            if (IsDragging && !HandleFollower.Instance.IsResize())
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                worldPosition.z = 0; // Z軸の値を0に設定（2Dゲームの場合）  
                transform.position = worldPosition - offSet; // 修正: Vector3 型のオフセットを使用  
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
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            offSet = worldPosition - transform.position; // 修正: Vector3 型のオフセットを計算  
            Inspector.Instance.Choice(stageBlockData);
        }
    }
}
