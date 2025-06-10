using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.InGame;
using System.IO;

namespace ShotBall.Data
{
    public class StageData : MonoBehaviour
    {
        float cameraSize = 5f;
        public BlockData[] Datas;

        public void Save()
        {
            ObjectBase[] gameObjects = GetComponentsInChildren<ObjectBase>();
            Datas = new BlockData[gameObjects.Length];
            for (int i = 0; i < gameObjects.Length; i++)
            {
                Datas[i] = new BlockData(gameObjects[i].gameObject);
            }
            cameraSize = Camera.main.orthographicSize;
            CreateFile();
        }

        public void CreateFile()
        {

            string json = JsonUtility.ToJson(new BlockDataWrapper { Blocks = Datas,cameraSize=cameraSize }, true);
            string path = Path.Combine(Application.dataPath, "StageData/StageData.json");
            File.WriteAllText(path, json);

            Debug.Log("Stage data saved to: " + path);
        }

        [System.Serializable]
        private class BlockDataWrapper
        {
            public BlockData[] Blocks;
            public float cameraSize;    
        }
    }
}
