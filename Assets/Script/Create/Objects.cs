using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ShotBall.Data;
using System;

namespace ShotBall.Create
{
    public class Objects : MonoBehaviour
    {
        [SerializeField] List<CreateObject> objects = new List<CreateObject>();

        [SerializeField] string fileName;

        [SerializeField]Stage stage;

        [SerializeField] FrameManager frameManager;


        public CreateObject[] CreateObjects => objects.ToArray();

        public Action UpdateObjects;
        public void Add(CreateObject obj)
        {
            objects.Add(obj);
            UpdateObjects?.Invoke();
        }
        private void Start()
        {
            UpdateList();
            StartCoroutine(Coroutine());
        }
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(1f);
            CreateFile();
        }
        public void CreateFile()
        {
            Debug.Log("Objects Count: " + objects.Count+"Frames Length"+frameManager.Frames.Length);

            string json = JsonUtility.ToJson(Data(), true);
            Texture2D texture2D= stage.CaptureArea();

            SaveManager.Save($"StageData.{fileName}", json, texture2D);
        }
        public BlockDataWrapper Data()
        {
            foreach (var obj in objects)
            {
                obj.SaveData();
            }
            List<BlockData> datas = new List<BlockData>();
            for (int i = 0; i < objects.Count; i++)
            {
                datas.Add( DataChange.ChangeBlockData(objects[i].StageBlockData));
            }
            for (int i = 0; i < frameManager.Frames.Length; i++)
            {
                for(int j=0;j<frameManager.Frames[i].BlockNumber-1; j++)
                {
                    datas.Add( DataChange.ChangeBlockData(frameManager.Frames[i].StageBlockData));
                }
            }
            return new BlockDataWrapper
            {
                Blocks = datas.ToArray(),
                cameraSize = stage.CameraSize,
                centerWorldWidth = stage.CenterWorldWidth,
                //keyNumberData = KeyNumberManager.GetKeyNumberData()
            };
        }
        public void UpdateList()
        {
            objects.Clear();
            Debug.Log("Update List");
            foreach (Transform c in transform)
            {
                if (c == null) continue; // Destroy済みならここでスキップ

                CreateObject obj = c.GetComponent<CreateObject>();
                if (obj != null) objects.Add(obj);
            }

            UpdateObjects?.Invoke();
        }
    }
}
