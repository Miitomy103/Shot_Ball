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

        int blockCount = 1;
        public int BlockCount => blockCount;

        IChangeText changeText;

        Type[] keepTypes = new Type[]
        {
            typeof(Transform),
            typeof(SpriteRenderer)
        };


        [SerializeField] private BlockData blockDataPreview;

        StageBlockData stageBlockData;
        public StageBlockData StageBlockData => stageBlockData;

        public bool IsSameFrame(CreateObject createObject)
        {
            return createObject != null
                && stageBlockData != null
                && stageBlockData.Type == createObject.StageBlockData.Type
                && stageBlockData.GetVector3Parameter("Scale") == createObject.StageBlockData.GetVector3Parameter("Scale")
                && stageBlockData.GetVector2Parameter("Size") == createObject.StageBlockData.GetVector2Parameter("Size");
        }

        public Action DeleteAction { get; set; }

        public bool isDestroy;

        private void Awake()
        {
            thisSprite = GetComponent<SpriteRenderer>();
            changeText = GetComponentInChildren<IChangeText>();
        }
        private void Start()
        {
            changeText.ChangeText(blockCount.ToString());
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
            blockDataPreview = DataChange.ChangeBlockData(stageBlockData);

            GameObject copy = Instantiate(createObject.gameObject,transform);
            copy.name = "Copy" + copy.name;

            var allComponents = copy.GetComponents<Component>();

            foreach (var comp in allComponents)
            {
                if (!keepTypes.Contains(comp.GetType()))
                {
                    DestroyImmediate(comp);
                }
            }
            FitSpriteInSquare(copy.GetComponent<SpriteRenderer>());
        }

        public void Plus()
        {
            blockCount++;
            changeText.ChangeText(blockCount.ToString());
        }
        public void Minus()
        {
            blockCount--;
            changeText.ChangeText(blockCount.ToString());
            if (blockCount <= 0) Delete();
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
            if (max == 0) obj.transform.localScale = Vector2.zero;
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


            Bounds bounds = thisSprite.bounds;


            if (bounds.Contains(mouseWorldPos))
            {
                Inspector.Instance.Choice(stageBlockData);
                Debug.Log("Frame Selected");
            }
        }
    }
}
