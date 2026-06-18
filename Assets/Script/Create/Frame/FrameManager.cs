using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// Editモードでのフレームを管理するクラス
    /// </summary>
    public class FrameManager : MonoBehaviour, IFrameManager
    {
        private static FrameManager instance;
        public static FrameManager Instance => instance;

        [SerializeField] GameObject framePrefab;

        [SerializeField] int maxFrame = 6;

        [SerializeField] Stage stage;

        [SerializeField] GameObject frameRangeSprite;
        [SerializeField] List<Frame> frames = new List<Frame>();

        [SerializeField] BlockType[] prohibitedTypes;

        public Frame[] Frames => frames.ToArray();


        [Header("Debug")]
        [SerializeField] CreateObject CreateObject;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        /// <summary>
        /// CreateObjectをフレームに追加する
        /// </summary>
        public void InObject(CreateObject createObject)
        {
            if (createObject == null) return;

            bool isExist = false;
            foreach (var type in prohibitedTypes)
            {
                if (createObject.StageBlockData.Type == type)
                {
                    isExist = true;
                    LogDisplay.Instance.SetLog("このオブジェクトはフレームに入れられません");
                }
            }

            if (isExist)
            {
                createObject.transform.position = new Vector3(00, 00, CreateObject.dragZ);
                HandleFollower.Instance.SpriteSet(null, false);
                return;
            }

            createObject.InFrame = true;
            createObject.SaveData();

            foreach (var existingFrame in frames)
            {
                if (existingFrame.IsSameFrame(createObject))
                {
                    existingFrame.Plus();
                    createObject.transform.position = new Vector3(00, 00, CreateObject.dragZ);
                    createObject.SetStoredInFrame(true);
                    HandleFollower.Instance.SpriteSet(null, false);
                    return;
                }
            }

            if (frames.Count >= maxFrame)
            {
                createObject.InFrame = false;
                createObject.SaveData();
                createObject.transform.position = new Vector3(00, 00, CreateObject.dragZ);
                HandleFollower.Instance.SpriteSet(null, false);
                return;
            }

            GameObject f = Instantiate(framePrefab, transform);
            Frame frame = f.GetComponent<Frame>();

            frames.Add(frame);
            frame.DeleteAction += FrameDelete;
            frame.Initialize(createObject);

            FramesPosition();

            createObject.transform.position = new Vector3(00, 00, CreateObject.dragZ);
            createObject.SetStoredInFrame(true);
            HandleFollower.Instance.SpriteSet(null, false);
        }
        void FrameDelete()
        {
            foreach (var f in frames)
            {
                if (f.isDestroy)
                {
                    frames.Remove(f);
                    Destroy(f.gameObject);
                    FramesPosition();
                    break;
                }
            }
        }
        public void FramesPosition()
        {
            float top = stage.TopY;
            float bottom = stage.BottomY;

            int count = frames.Count;

            float x = frameRangeSprite.transform.position.x;
            float z = 0;

            if (count <= 0)
            {
                Debug.LogWarning("オブジェクト数は1以上にしてください");
                return;
            }

            if (count == 1)
            {
                float centerY = (top + bottom) / 2f;
                frames[0].transform.position = new Vector3(x, centerY, z);
            }
            else
            {
                float interval = (top - bottom) / (count + 1);
                for (int i = 0; i < count; i++)
                {
                    float y = bottom + interval * (i + 1);
                    frames[i].transform.position = new Vector3(x, y, z);
                }
            }
        }


    }
}
