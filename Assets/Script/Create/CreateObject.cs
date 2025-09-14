using JetBrains.Annotations;
using ShotBall.Data;
using ShotBall.InGame;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShotBall.Create
{
    public class CreateObject : MonoBehaviour,IChangeSize,IStageBlockData
    {
        [SerializeField] private bool isSizeChange = true;

        SpriteRenderer ThisSprite;

        Vector3 offSet; // 修正: Vector2 から Vector3 に変更  

        public bool IsDragging { get; private set; } // ドラッグ中かどうか  

        [SerializeField] BlockType definitionType;

        [SerializeField] private StageBlockData stageBlockData;
        public StageBlockData StageBlockData => stageBlockData;

        [SerializeField, ReadOnly] string gimmickName;

        [SerializeField]Parametors parametors;


        public Action DragObject { get; set; } = delegate { };

        public int BlockId;

        public bool InFrame { get; set; }

        public const float dragZ = -5f; // Inspector から変更可能にする
        private void Awake()
        {
            ThisSprite = GetComponent<SpriteRenderer>();

            if(parametors==null)Debug.LogError("Parametors not found"+gameObject.name);
            stageBlockData = new StageBlockData(definitionType,parametors);
            Debug.Log(stageBlockData.isDataSet + gameObject.name);
            gimmickName = stageBlockData.Type.ToString();
        }
        public void Load(StageBlockData data)
        {
            stageBlockData = new StageBlockData(data);
            transform.localScale = stageBlockData.GetVector3Parameter("Scale");
            ThisSprite.size = stageBlockData.GetVector2Parameter("Size");
            transform.position = stageBlockData.GetVector3Parameter("Position");
            transform.eulerAngles = stageBlockData.GetVector3Parameter("Rotation");

            Color c = ThisSprite.color;
            c.r = stageBlockData.GetFloatParameter("ColorR");
            c.g = stageBlockData.GetFloatParameter("ColorG");
            c.b = stageBlockData.GetFloatParameter("ColorB");
            c.a = stageBlockData.GetFloatParameter("ColorA");
            ThisSprite.color = c;

            InFrame = stageBlockData.GetBoolParameter("InFrame");
            BlockId = stageBlockData.GetIntParameter("BlockId");
        }
        private void Update()
        {
            if(IsDragging|| HandleFollower.Instance.ResizeNow()) DragObject?.Invoke();
            if (IsDragging && !HandleFollower.Instance.ResizeNow())
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z; // カメラからの距離
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                worldPosition.z = dragZ; // Z を任意に設定
                transform.position = worldPosition - offSet;
                DragObject?.Invoke();

                if (Input.GetMouseButtonUp(0)) // 左クリックを離したとき
                {
                    IsDragging = false;
                    if (MouseEnter.inMouse)
                    {
                        FrameManager.Instance.InObject(this);
                    }
                }
            }
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
                offSet = Vector3.zero;
            Debug.Log("CreateObject OnMouseDown");

            HandleFollower.Instance.SpriteSet(ThisSprite, isSizeChange);
            if (HandleFollower.Instance.ResizeNow()||ConnectionManager.isConnect) return;

            IsDragging = true;

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            offSet = worldPosition - transform.position;
            offSet.z = 0; // オフセットは常に0にしておくと扱いやすい

            Inspector.Instance.Choice(stageBlockData);
            DragObject?.Invoke();
        }



        public void ChangeSize()
        {
            stageBlockData.SetVector2Parameter("Size", ThisSprite.size, false);
        }
        public void ChangePosition()
        {
            stageBlockData.SetVector3Parameter("Position", transform.position, false);
            stageBlockData.SetVector3Parameter("Rotation", transform.eulerAngles, false);
        }
        public void SaveData()
        {
            if(ThisSprite == null) ThisSprite = GetComponent<SpriteRenderer>();
            stageBlockData.SetVector3Parameter("Scale", transform.localScale,false);  
            stageBlockData.SetVector2Parameter("Size", ThisSprite.size, false);
            stageBlockData.SetVector3Parameter("Position", (Vector2)transform.position, false);
            stageBlockData.SetVector3Parameter("Rotation", transform.eulerAngles, false);
            stageBlockData.SetFloatParameter("ColorR", ThisSprite.color.r,false);
            stageBlockData.SetFloatParameter("ColorG", ThisSprite.color.g, false);
            stageBlockData.SetFloatParameter("ColorB", ThisSprite.color.b, false);
            stageBlockData.SetFloatParameter("ColorA", ThisSprite.color.a, false);
            stageBlockData.SetBoolParameter("InFrame", InFrame, false);
            stageBlockData.SetIntParameter("BlockId", BlockId, false);

            if (TryGetComponent<Connect>(out var component))
            {
                for (int i = 0; i < component.connectUIs.Count; i++)
                {
                    stageBlockData.SetIntParameter("KeyNumber" + i.ToString(), component.connectUIs[i].createObject.BlockId, false);
                }
            }
        }
        private void OnDestroy()
        {
            Debug.Log("CreateObject OnDestroy");
        }
    }
}
