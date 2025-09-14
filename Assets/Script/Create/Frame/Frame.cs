using ShotBall.InGame;
using System;
using System.Linq;
using UnityEngine;

namespace ShotBall.Create
{
    public class Frame : MonoBehaviour,IStageBlockData
    {
        public SpriteRenderer objectSprite;

        SpriteRenderer thisSprite;

        int blockNumber = 1;
        public int BlockNumber => blockNumber;

        IChangeText changeText;

        // 残したいコンポーネントの型（複数可）
        Type[] keepTypes = new Type[]
        {
            typeof(Transform),
            typeof(SpriteRenderer) // ←残したいコンポーネントを追加
        };


        [SerializeField] private BlockData blockDataPreview; // デバッグ用

        StageBlockData stageBlockData;
        public StageBlockData StageBlockData => stageBlockData;

        public Action DeleteAction { get; set; }

        public bool isDestroy;

        private void Awake()
        {
            thisSprite = GetComponent<SpriteRenderer>();
            changeText = GetComponentInChildren<IChangeText>();
        }
        private void Start()
        {
            changeText.ChangeText(blockNumber.ToString());
        }
        private void Update()
        {
            if (MouseInputHandler.Instance.LeftDown&&MouseEnter.inMouse)
            {
                OnMouseDownEvent();
            }

        }
        public void Initialized(CreateObject createObject)
        {
            stageBlockData = new StageBlockData(createObject.StageBlockData);
            blockDataPreview = DataChange.ChangeBlockData(stageBlockData); // デバッグ用

            GameObject copy = Instantiate(createObject.gameObject,transform);
            copy.name = "Copy" + copy.name;

            var allComponents = copy.GetComponents<Component>();

            foreach (var comp in allComponents)
            {
                if (!keepTypes.Contains(comp.GetType()))
                {
                    DestroyImmediate(comp); // エディタ上でも即座に消したい場合はDestroyImmediate
                }
            }
            FitSpriteInSquare(copy.GetComponent<SpriteRenderer>());
        }

        public void Plus()
        {
            blockNumber++;
            changeText.ChangeText(blockNumber.ToString());
        }
        public void Minus()
        {
            blockNumber--;
            changeText.ChangeText(blockNumber.ToString());
            if (blockNumber <= 0) Delete();
        }
        public void Delete()
        {
            isDestroy = true;
            DeleteAction();
        }
        public void FitSpriteInSquare(SpriteRenderer obj)
        {
            Vector2 input = obj.bounds.size;

            float max = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
            if (max == 0) obj.transform.localScale = Vector2.zero; // 0除算防止
            obj.transform.localScale *= thisSprite.bounds.size.x * 0.8f / max;
            obj.transform.position = thisSprite.transform.position;

            obj.sortingOrder = 2;
        }
        public void Save()
        {
            stageBlockData.SetBoolParameter("InFrame", true, false);
        }
        public void OnMouseDownEvent()
        {
            Debug.Log("Frame Clicked");
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = thisSprite.transform.position.z;


            // Spriteの矩形範囲を取得
            Bounds bounds = thisSprite.bounds;


            // 境界内にマウス座標があるか判定
            if (bounds.Contains(mouseWorldPos))
            {
                Inspector.Instance.Choice(stageBlockData);
                Debug.Log("Frame Selected");
            }
        }
    }
}
