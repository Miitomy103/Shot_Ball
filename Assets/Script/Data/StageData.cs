using ShotBall.InGame;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ShotBall.Data
{
    public class StageData : MonoBehaviour
    {
        float cameraSize = 5f;
        public BlockData[] Datas;

        [SerializeField] PrefabDatas prefabDatas;

        [SerializeField] List<GameObject> GenerateObjcts;
        [SerializeField] string loadStageName;

        [SerializeField] KeyNumberManager KeyNumberManager;
        [SerializeField] CameraManager cameraManager;
        [SerializeField] GameLoop GameLoop;
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
            string json = JsonUtility.ToJson(new BlockDataWrapper {
                Blocks = Datas,
                cameraSize=cameraSize,
                centerWorldWidth = cameraManager.CenterWorldWidth,
                keyNumberData = KeyNumberManager.GetKeyNumberData()
            }, true);
            string path = Path.Combine(Application.dataPath, $"StageData/StageData.{SceneControl.NowStage()}.json");
            File.WriteAllText(path, json);

            Debug.Log("Stage data saved to: " + path);
        }

        public void LoadFile()
        {
            DelateObject();
            //GenerateObjcts= new List<GameObject>();
            GameLoop.launchPads.Clear();

            string path = Path.Combine(Application.dataPath, $"StageData/StageData.{loadStageName}.json");

            if (!File.Exists(path))
            {
                Debug.LogError("ファイルが見つかりません: " + path);
                return;
            }

            string json = File.ReadAllText(path);
            BlockDataWrapper wrapper = JsonUtility.FromJson<BlockDataWrapper>(json);

            Datas = wrapper.Blocks;
            BlockData[] datas = wrapper.Blocks;
            cameraManager.CenterWorldWidth = wrapper.centerWorldWidth;
            KeyNumberManager.LoadKeyNumber(wrapper.keyNumberData);

            Camera.main.orthographicSize = wrapper.cameraSize;
            for (int i=0;i<datas.Length;i++)
            {
                ObjectGenerate(datas[i]);
            }
        }
        void ObjectGenerate(BlockData data)
        {
            for(int i=0;i<prefabDatas.prefabs.Length; i++)
            {
                if (data.GimickName == prefabDatas.prefabs[i].name)
                {
                    GameObject obj = Instantiate(prefabDatas.prefabs[i].PrefabObj, data.Position, data.Rotation);
                    GenerateObjcts.Add(obj);
                    obj.transform.localScale = data.Scale;
                    if(obj.TryGetComponent<GimmickBase>(out var gimmick))
                    {
                        gimmick.LoadData(data.GimmickData);
                        if(gimmick is LaunchPad launchPad)
                        {
                            GameLoop.launchPads.Add(launchPad);
                            string[] datas = data.GimmickData.Split(',');
                            int keyNumber = int.Parse(datas[1]);
                            KeyNumberManager.KeyNumbers[keyNumber].launchPads.Add(launchPad);
                        }
                        if(gimmick is CrearArea crearArea)
                        {
                            string[] datas = data.GimmickData.Split(',');
                            int keyNumber = int.Parse(datas[0]);
                            KeyNumberManager.KeyNumbers[keyNumber].crearAreas.Add(crearArea);
                        }
                    }
                    SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
                    sprite.size = data.size;
                    sprite.color = data.color;
                    ObjectType type = data.Type;
                    ObjectBase objectBase = type switch
                    {
                        ObjectType.Runtime => obj.AddComponent<RuntimeDragObject>(),
                        ObjectType.Editor => obj.AddComponent<EditorDragObject>(),
                        ObjectType.Children => obj.AddComponent<ChildrenDragObject>(),
                        _ => throw new System.InvalidOperationException("Unknown object type.")
                    };
                    DragObjectSelector selector = obj.GetComponent<DragObjectSelector>();
                    if (selector != null) DestroyImmediate(selector);
                    if (data.childData.Length>0)
                    {
                        for (int k = 0; k < data.childData.Length; k++)
                        {
                            ObjectGenerate(data.childData[k]);
                        }
                    }
                }
            }
        }
        public void DelateObject()
        {
            for (int i = 0; i < GenerateObjcts.Count; i++)
            {
                if (GenerateObjcts[i] != null)
                {
                    DestroyImmediate(GenerateObjcts[i]);
                }
            }
            GenerateObjcts.Clear();
        }
        [System.Serializable]
        private class BlockDataWrapper
        {
            public BlockData[] Blocks;
            public float cameraSize;
            public float centerWorldWidth;
            public KeyNumberData keyNumberData;
        }
    }
}
