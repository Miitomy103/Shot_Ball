using ShotBall.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall
{
    /// <summary>
    /// 1つのギミック・ブロックが持つパラメータ群（float/int/bool/Vector2/Vector3/ユニーク表示）を
    /// 種類別の辞書として保持するデータクラス。
    /// </summary>
    [System.Serializable]
    public class StageBlockData
    {
        public BlockType Type { get; private set; } // Default to Block type
        public Dictionary<string, float> floatParameters { get;private set; } = new Dictionary<string, float>();
        public Dictionary<string, int> intParameters { get;private set; } = new Dictionary<string, int>();
        public Dictionary<string, bool> boolParameters { get;private set; } = new Dictionary<string, bool>();
        public Dictionary<string, Vector2> vector2Parameters { get; private set; } = new Dictionary<string, Vector2>();
        public Dictionary<string, Vector3> vector3Parameters { get;private set; } = new Dictionary<string, Vector3>();
        public Dictionary<string, ViewType> uniqueViews { get;private set; } = new Dictionary<string, ViewType>();

        public Dictionary<string,bool>isDisplays = new Dictionary<string, bool>();

        public Action floatSet;
        public Action intSet;
        public Action boolSet;
        public Action vector2Set;
        public Action vector3Set;
        public Action viewSet;

        public bool isDataSet;

        [SerializeField, ReadOnly] string key;
        public StageBlockData() { }

        /// <summary>
        /// 既存のStageBlockDataの内容をコピーして新しいインスタンスを作成する。
        /// </summary>
        public StageBlockData(StageBlockData data)
        {
            Type = data.Type;
            floatParameters = new Dictionary<string, float>(data.floatParameters);
            intParameters = new Dictionary<string, int>(data.intParameters);
            boolParameters = new Dictionary<string, bool>(data.boolParameters);
            vector2Parameters = new Dictionary<string, Vector2>(data.vector2Parameters);
            vector3Parameters = new Dictionary<string, Vector3>(data.vector3Parameters);
            uniqueViews = new Dictionary<string, ViewType>(data.uniqueViews);
            isDisplays = new Dictionary<string, bool>(data.isDisplays);
            isDataSet = data.isDataSet;
        }
        /// <summary>
        /// ブロック定義(BlockDefinitionSO)からデフォルトパラメータを読み込んで初期化する。
        /// </summary>
        public StageBlockData(BlockType definitionType, BlockDefinitionSO[] parameters)
        {
            Type = definitionType;

            if (parameters == null) return;

            foreach (var p in parameters)
            {
                if (definitionType != p.type) continue;
                foreach (var d in p.floatParameters) floatParameters.Add(d.key, d.defaultValue);
                foreach (var d in p.intParameters) intParameters.Add(d.key, d.defaultValue);
                foreach (var d in p.boolParameters) boolParameters.Add(d.key, d.defaultValue);
                foreach (var d in p.vector2Parameters) vector2Parameters.Add(d.key, d.defaultValue);
                foreach (var d in p.vector3Parameters) vector3Parameters.Add(d.key, d.defaultValue);
                foreach (var d in p.uniqueViews) uniqueViews.Add(d.key, d.defaultValue);
                isDataSet = true;
                Debug.Log("データセット");
            }
        }

        public void SetType(BlockType type)
        {
            Type = type;
        }

        public void SetFloatParameter(string key, float value,bool isDisplay=true)
        {
            floatParameters[key] = value;
            isDisplays[key] = isDisplay;
            floatSet?.Invoke();
        }
        public void SetIntParameter(string key, int value, bool isDisplay = true)
        {
            intParameters[key] = value;
            isDisplays[key] = isDisplay;
            intSet?.Invoke();
        }
        public void SetBoolParameter(string key, bool value, bool isDisplay = true)
        {
            boolParameters[key] = value;
            isDisplays[key] = isDisplay;
            boolSet?.Invoke();
        }
        public void SetVector2Parameter(string key, Vector2 value, bool isDisplay = true)
        {
            vector2Parameters[key] = value;
            isDisplays[key] = isDisplay;
            vector2Set?.Invoke();
        }
        public void SetVector3Parameter(string key, Vector3 value, bool isDisplay = true)
        {
            vector3Parameters[key] = value;
            isDisplays[key] = isDisplay;
            vector3Set?.Invoke();
        }
        public void SetUniqueView(string key, ViewType viewType)
        {
            uniqueViews[key] = viewType;
            viewSet?.Invoke();
        }


        public float GetFloatParameter(string key)
        {
            if (floatParameters.TryGetValue(key, out float value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"Float parameter '{key}' not found.");
                return default;
            }
        }
        public int GetIntParameter(string key)
        {
            if (intParameters.TryGetValue(key, out int value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"Int parameter '{key}' not found.");
                return default;
            }
        }
        public bool GetBoolParameter(string key)
        {
            if (boolParameters.TryGetValue(key, out bool value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"Bool parameter '{key}' not found.");
                return default;
            }
        }
        public Vector2 GetVector2Parameter(string key)
        {
            if (vector2Parameters.TryGetValue(key, out Vector2 value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"Vector2 parameter '{key}' not found.");
                return default;
            }
        }
        public Vector3 GetVector3Parameter(string key)
        {
            if (vector3Parameters.TryGetValue(key, out Vector3 value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"Vector3 parameter '{key}' not found.");
                return default;
            }
        }
        public ViewType GetUniqueView(string key)
        {
            if (uniqueViews.TryGetValue(key, out ViewType value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning($"Unique view '{key}' not found.");
                return default;
            }
        }
    }

}
