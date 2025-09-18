using System.IO;
using UnityEditor;
using UnityEngine;

namespace ShotBall.Create
{
    public class DefaultData : MonoBehaviour
    {
        public BlockDataWrapper defaultData=>LoadFile();

        [SerializeField] string fileName;

        [SerializeField]Objects objects;

        public void ResetData()
        {
            BlockDataWrapper data = new BlockDataWrapper();
            data =objects.Data();
            string json = JsonUtility.ToJson(data, true);
            string path = Application.streamingAssetsPath + $"/{fileName}.json";
            File.WriteAllText(path, json);

            Debug.Log("Default data reset to: " + path);
        }
        BlockDataWrapper LoadFile()
        {
            // ファイルから読み込み
            string path = Application.streamingAssetsPath + $"/{fileName}.json";
            string json = File.ReadAllText(path);

            // JSON → クラスに変換
            return JsonUtility.FromJson<BlockDataWrapper>(json);
        }
    }





}
