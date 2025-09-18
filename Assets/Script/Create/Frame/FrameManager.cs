using ShotBall.Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.Create
{
    public class FrameManager : MonoBehaviour
    {
        static FrameManager instance;
        public static FrameManager Instance { get { return instance; } }

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
            instance = this;
        }
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

            if (frames.Count >= maxFrame||isExist)
            {
                createObject.transform.position = new Vector3(00, 00, CreateObject.dragZ);
                HandleFollower.Instance.SpriteSet(null, false);
                return;
            }
            

            createObject.InFrame = true;

            createObject.SaveData();

            GameObject f= Instantiate(framePrefab,transform);

            Frame frame = f.GetComponent<Frame>();

            frames.Add(frame);
            frame.DeleteAction += FrameDelete;
            frame.Initialized(createObject);

            FramesPosition();

            createObject.transform.position= new Vector3(00, 00, CreateObject.dragZ);
            HandleFollower.Instance.SpriteSet(null, false);
        }
        void FrameDelete()
        {
            foreach(var f in frames)
            {
                if(f.isDestroy)
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
            float top = stage.topY;
            float bottom = stage.bottonY;

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
