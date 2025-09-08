using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.Create;
using ShotBall.Data;
using ShotBall.InGame;
using System;

namespace ShotBall
{
    [System.Serializable]
    public class StageBlockData
    {
        public BlockType Type { get; private set; } // Default to Block type
        public Dictionary<string, float> floatParametors { get;private set; } = new Dictionary<string, float>();
        public Dictionary<string, int> intParametors { get;private set; } = new Dictionary<string, int>();
        public Dictionary<string, bool> boolParametors { get;private set; } = new Dictionary<string, bool>();
        public Dictionary<string, Vector2> vector2Parametors { get;private set; }= new Dictionary<string, Vector2>();
        public Dictionary<string, Vector3> vector3Parametors { get;private set; } = new Dictionary<string, Vector3>();
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

        public StageBlockData(StageBlockData data)
        {
            Type = data.Type;
            floatParametors = new Dictionary<string, float>(data.floatParametors);
            intParametors = new Dictionary<string, int>(data.intParametors);
            boolParametors = new Dictionary<string, bool>(data.boolParametors);
            vector2Parametors = new Dictionary<string, Vector2>(data.vector2Parametors);
            vector3Parametors = new Dictionary<string, Vector3>(data.vector3Parametors);
            uniqueViews = new Dictionary<string, ViewType>(data.uniqueViews);
            isDisplays = new Dictionary<string, bool>(data.isDisplays);
            isDataSet = data.isDataSet;
        }
        public StageBlockData(BlockType definitionType,Parametors parametors)
        {
            Type = definitionType;

            if (parametors == null) return;

            foreach (var p in parametors.blockDefinitions)
            {
                if (definitionType != p.type) continue;
                foreach (var d in p.floatParameters) floatParametors.Add(d.key, d.defaultValue);
                foreach (var d in p.intParameters) intParametors.Add(d.key, d.defaultValue);
                foreach (var d in p.boolParameters) boolParametors.Add(d.key, d.defaultValue);
                foreach (var d in p.vector2Parameters) vector2Parametors.Add(d.key, d.defaultValue);
                foreach (var d in p.vector3Parameters) vector3Parametors.Add(d.key, d.defaultValue);
                foreach (var d in p.uniqueViews) uniqueViews.Add(d.key, d.viewType);
                isDataSet = true;
                Debug.Log("データセット");
            }
            //key = vector2Parametors.ToString();
        }

        public void SetType(BlockType type)
        {
            Type = type;
        }

        public void SetFloatParameter(string key, float value,bool isDisplay=true)
        {
            floatParametors[key] = value;
            isDisplays[key] = isDisplay;
            floatSet?.Invoke();
        }
        public void SetIntParameter(string key, int value, bool isDisplay = true)
        {
            intParametors[key] = value;
            isDisplays[key] = isDisplay;
            intSet?.Invoke();
        }
        public void SetBoolParameter(string key, bool value, bool isDisplay = true)
        {
            boolParametors[key] = value;
            isDisplays[key] = isDisplay;
            boolSet?.Invoke();
        }
        public void SetVector2Parameter(string key, Vector2 value, bool isDisplay = true)
        {
            vector2Parametors[key] = value;
            isDisplays[key] = isDisplay;
            vector2Set?.Invoke();
        }
        public void SetVector3Parameter(string key, Vector3 value, bool isDisplay = true)
        {
            vector3Parametors[key] = value;
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
            if (floatParametors.TryGetValue(key, out float value))
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
            if (intParametors.TryGetValue(key, out int value))
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
            if (boolParametors.TryGetValue(key, out bool value))
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
            if (vector2Parametors.TryGetValue(key, out Vector2 value))
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
            if (vector3Parametors.TryGetValue(key, out Vector3 value))
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
