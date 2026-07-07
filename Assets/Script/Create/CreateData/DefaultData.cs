using ShotBall.Data;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// デフォルトのステージデータを保存・ロードするクラス
    /// </summary>
    public class DefaultData : MonoBehaviour
    {
        public BlockDataWrapper defaultData => LoadFile();

        [SerializeField] string fileName;

        [SerializeField] Objects objects;

        public void ResetData()
        {
            BlockDataWrapper data = objects.Data();
            string json = JsonUtility.ToJson(data, true);
#if UNITY_EDITOR
            string path = Application.dataPath + $"/Resources/{fileName}.json";
            System.IO.File.WriteAllText(path, json);
            UnityEditor.AssetDatabase.Refresh();
            Debug.Log("Default data reset to: " + path);
#else
            Debug.LogWarning("ResetData is editor-only.");
#endif
        }

        BlockDataWrapper LoadFile()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            if (textAsset == null)
            {
                Debug.LogError($"Resources にファイルが見つかりません: {fileName}");
                return null;
            }
            return JsonUtility.FromJson<BlockDataWrapper>(textAsset.text);
        }
    }
}
