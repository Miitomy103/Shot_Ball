using ShotBall;
using ShotBall.InGame;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ShotBall.Data
{
    /// <summary>
    /// 1.ステージのデータを保存・ロードするクラス
    /// </summary>
    public class StageData : MonoBehaviour
    {
        [Header("LoadSettings")]
        [SerializeField]bool autoLoad = false;
        [SerializeField] bool loadStage = false;

        float cameraSize = 5f;
        private BlockData[] dataList;

        

        [SerializeField] PrefabLists prefabDataList;

        [SerializeField] List<GameObject> GenerateObjects;
        [SerializeField] string loadStageName;

        [SerializeField] KeyNumberManager KeyNumberManager;
        [SerializeField] CameraManager cameraManager;
        [SerializeField] GameLoop GameLoop;

        [SerializeField] Transform runtimeParent;
        [SerializeField] Transform editorParent;

        private void Awake()
        {
            if (autoLoad) LoadFile();
        }
        public void Save()
        {
            ObjectBase[] gameObjects = GetComponentsInChildren<ObjectBase>();
            dataList = new BlockData[gameObjects.Length];
            for (int i = 0; i < gameObjects.Length; i++)
            {
                dataList[i] = new BlockData(gameObjects[i].gameObject);
            }
            cameraSize = Camera.main.orthographicSize;
            CreateFile();
        }


        public void CreateFile()
        {
            string json = JsonUtility.ToJson(new BlockDataWrapper {
                Blocks = dataList,
                cameraSize=cameraSize,
                centerWorldWidth = cameraManager.CenterWorldWidth,
                //keyNumberData = KeyNumberManager.GetKeyNumberData()
            }, true);
            string path = Path.Combine(Application.dataPath, $"StageData/StageData.{SceneControl.NowStage()}.json");
            File.WriteAllText(path, json);

            Debug.Log("Stage data saved to: " + path);
        }

        public void LoadFile()
        {
            DelateObject();
            //GenerateObjects= new List<GameObject>();
            GameLoop.launchPads.Clear();

            BlockDataWrapper wrapper;
            if (loadStageName=="StaticData")
            {
                wrapper= StaticData.blockDataWrapper;
            }
            else
            {
                string path = $"StageData.{loadStageName}";

                (string a, Texture2D t) = SaveManager.Load(path);
                wrapper = JsonUtility.FromJson<BlockDataWrapper>(a);
            }

            if (loadStage)
            {
                if(LoadScene.BlockDataWrapper != null)
                {
                    wrapper = LoadScene.BlockDataWrapper;
                }
            }

            if(wrapper == null)
            {
                Debug.LogError("Failed to load stage data.");
                return;
            }

            this.dataList = wrapper.Blocks;
            BlockData[] datas = wrapper.Blocks;
            cameraManager.CenterWorldWidth = wrapper.centerWorldWidth;

            Camera.main.orthographicSize = wrapper.cameraSize;
            
            foreach(var data in datas)
            {
                ObjectGenerate(data);
            }
        }
        void ObjectGenerate(BlockData data)
        {
            for (int i = 0; i < prefabDataList.prefabs.Length; i++)
            {
                if (data.BlockType == prefabDataList.prefabs[i].type)
                {
                    Debug.Log("Generate Object: " + data.BlockType + "prefabDara"+prefabDataList.prefabs[i].type);
                    GameObject obj = Instantiate(prefabDataList.prefabs[i].PrefabObj, data.Position, data.Rotation);
                    
                    obj.transform.parent = data.Type switch
                    {
                        ObjectType.Runtime => runtimeParent,
                        ObjectType.Editor => editorParent,
                        ObjectType.Children => null,
                        _ => throw new System.InvalidOperationException("Unknown object type.")
                    };
                    GenerateObjects.Add(obj);

                    obj.transform.localScale = data.Scale;
                    if (obj.TryGetComponent<GimmickBase>(out var gimmick))
                    {
                         gimmick.LoadData(data.GimmickData);
                         gimmick.KeyNumberLoad(data.BlockIds);
                    }
                    if(obj.TryGetComponent<ObjectBase>(out var objectB))
                    {
                        objectB.BlockId = data.BlockId;
                    }
                    SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
                    sprite.color = data.color;

                    if(obj.TryGetComponent<SpriteImage>(out var spriteImage))
                    {
                        spriteImage.SizeChange(data.size);
                    }

                    ObjectType type = data.Type;
                    ObjectBase objectBase = type switch
                    {
                        ObjectType.Runtime => obj.AddComponent<RuntimeDragObject>(),
                        ObjectType.Editor => obj.AddComponent<EditorDragObject>(),
                        ObjectType.Children => obj.AddComponent<ChildrenDragObject>(),
                        _ => throw new System.InvalidOperationException("Unknown object type.")
                    };

                    DragObjectSelector selector = obj.GetComponent<DragObjectSelector>();
                    if (selector != null) Destroy(selector);

                    if (data.childData == null) return;

                    if (data.childData.Length > 0)
                    {
                        for (int k = 0; k < data.childData.Length; k++)
                        {
                            ObjectGenerate(data.childData[k]);
                        }
                    }
                    return;
                }
            }
        }

        public void DelateObject()
        {
            for (int i = 0; i < GenerateObjects.Count; i++)
            {
                if (GenerateObjects[i] != null)
                {
                    DestroyImmediate(GenerateObjects[i]);
                }
            }
            GenerateObjects.Clear();
        }

    }

    /// <summary>
    /// 1ステージをまとめたデータクラス
    /// </summary>
    [System.Serializable]
    public class BlockDataWrapper
    {
        public BlockData[] Blocks;
        public float cameraSize;
        public float centerWorldWidth;
        public KeyNumberDataList keyNumberDataList;
    }
}
