using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ShotBall.Data;
using System;

namespace ShotBall.Create
{
    /// <summary>
    /// Editモードでのオブジェクトを管理するクラス
    /// </summary>
    public class Objects : MonoBehaviour
    {
        [SerializeField] List<CreateObject> objects = new List<CreateObject>();

        [SerializeField] Stage stage;

        [SerializeField] FrameManager frameManager;

        [SerializeField] GenerateObject GenerateObject;

        [Header("Save/Load Settings")]
        [SerializeField] bool isDefault;
        [SerializeField] bool isLoad = true;


        public CreateObject[] CreateObjects => objects.ToArray();

        /// <summary>
        /// objectsが更新されたときに呼ばれるイベント
        /// </summary>
        public event Action UpdateObjects;


        private void Awake()
        {
            LoadFile();
        }

        private void Start()
        {
            UpdateList();

            //StartCoroutine(Coroutine());
        }
        //デバッグ用
        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(1f);
            CreateFile();
        }
        public void Add(CreateObject obj)
        {
            objects.Add(obj);
            UpdateObjects?.Invoke();
        }
        /// <summary>
        /// ファイル作成
        /// </summary>
        public void CreateFile()
        {
            Debug.Log("Objects Count: " + objects.Count+"Frames Length"+frameManager.Frames.Length);

            //BlockDataWrapperをjsonに変換
            string json = JsonUtility.ToJson(Data(), true);
            //ステージの写真を撮る
            Texture2D texture2D= stage.CaptureArea();

            //セーブ
            SaveManager.Save($"StageData.{StageName.name}", json, texture2D);

            //ログ表示
            if(LogDisplay.Instance != null) LogDisplay.Instance.SetLog("セーブしました");
        }

        /// <summary>
        /// セーブ用のデータに変換
        /// </summary>
        public BlockDataWrapper Data()
        {
            //オブジェクトにデータを持たせる
            foreach (var obj in objects)
            {
                if (obj == null) continue;
                obj.SaveData();
            }

            List<BlockData> datas = new List<BlockData>();

            //フレームに入ってないオブジェクトのデータを取ってくる
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == null) continue;
                if (objects[i].StageBlockData.GetBoolParameter("InFrame")) continue;

                datas.Add(DataChange.ChangeBlockData(objects[i].StageBlockData));
            }

            //フレームに入ってるオブジェクトのデータを取ってくる
            for (int i = 0; i < frameManager.Frames.Length; i++)
            {
                for (int j = 0; j < frameManager.Frames[i].BlockCount; j++)
                {
                    datas.Add(DataChange.ChangeBlockData(frameManager.Frames[i].StageBlockData));
                }
            }

            //返す
            return new BlockDataWrapper
            {
                Blocks = datas.ToArray(),
                cameraSize = stage.CameraSize,
                centerWorldWidth = stage.CenterWorldWidth,
            };
        }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        public void LoadFile()
        {
            if(!isLoad) return;

            //データを持ってくる
            BlockDataWrapper data=LoadScene.BlockDataWrapper;
            if (data == null&&isDefault)
            {
                //なかった場合デフォルトになる
                data = GetComponent<DefaultData>().defaultData;
                if (data == null) { Debug.LogError("DefaultDataないよ"); return; }
            }

            //オブジェクトをリセット
            for (int i=0;i<objects.Count;i++)
            {
                if (objects[i] != null)
                {
                    Destroy(objects[i].gameObject);
                    objects.RemoveAt(i);
                }
            }
            objects.Clear();

            //データ変換
            BlockData[] datas = data.Blocks;
            //オブジェクト生成・配置
            foreach (var d in datas)
            {
                if (d.Type == ObjectType.Editor)
                {
                    GenerateObject.LoadObject(DataChange.ChangeStageBlockData(d));
                }
                else if(d.Type == ObjectType.Runtime)
                {
                    CreateObject c = GenerateObject.LoadObject(DataChange.ChangeStageBlockData(d));
                    frameManager.InObject(c);
                }
            }

            //ステージデータをセット
            stage.SizeSet(data.cameraSize, data.centerWorldWidth);
        }

        //デバッグ用
        IEnumerator WaitOneFrame(BlockDataWrapper data)
        {
            yield return null;
        }

        public void UpdateList()
        {
            objects.Clear();
            //Listなので1f待つ
            StartCoroutine(UpdateCoroutine());
        }
        IEnumerator UpdateCoroutine()
        {
            yield return null;
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
