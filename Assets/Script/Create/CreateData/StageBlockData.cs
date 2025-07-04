using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class StageBlockData:MonoBehaviour, IBlockData,IParametors
    {
        public BlockType definitionType; // ScriptableObject名
        public int blockId; // ブロックID
        public Dictionary<string, float> floatParametors = new Dictionary<string, float>();
        public Dictionary<string, int> intParametors = new Dictionary<string, int>();
        public Dictionary<string, bool> boolParametors = new Dictionary<string, bool>();
        public Dictionary<string, Vector2> vector2Parametors = new Dictionary<string, Vector2>();
        public Dictionary<string, Vector3> vector3Parametors = new Dictionary<string, Vector3>();

        private void Start()
        {
            float x = 1;
        }

        private void Awake()
        {
            foreach (var p in Parametors.blockDefinitions)
            {
                foreach(var d in p.floatParameters)floatParametors.Add(d.key,d.defaultValue);
                foreach (var d in p.intParameters) intParametors.Add(d.key, d.defaultValue);
                foreach (var d in p.boolParameters) boolParametors.Add(d.key, d.defaultValue);
                foreach (var d in p.vector2Parameters) vector2Parametors.Add(d.key, d.defaultValue);
                foreach (var d in p.vector3Parameters) vector3Parametors.Add(d.key, d.defaultValue);
            }
        }
        public void SetFloatParameter(string key, float value)
        {
            if (floatParametors.ContainsKey(key))
            {
                floatParametors[key] = value;
            }
            else
            {
                Debug.LogWarning($"Float parameter '{key}' not found.");
            }
        }
        public void SetIntParameter(string key, int value)
        {
            if (intParametors.ContainsKey(key))
            {
                intParametors[key] = value;
            }
            else
            {
                Debug.LogWarning($"Int parameter '{key}' not found.");
            }
        }
        public void SetBoolParameter(string key, bool value)
        {
            if (boolParametors.ContainsKey(key))
            {
                boolParametors[key] = value;
            }
            else
            {
                Debug.LogWarning($"Bool parameter '{key}' not found.");
            }
        }
        public void SetVector2Parameter(string key, Vector2 value)
        {
            if (vector2Parametors.ContainsKey(key))
            {
                vector2Parametors[key] = value;
            }
            else
            {
                Debug.LogWarning($"Vector2 parameter '{key}' not found.");
            }
        }
        public void SetVector3Parameter(string key, Vector3 value)
        {
            if (vector3Parametors.ContainsKey(key))
            {
                vector3Parametors[key] = value;
            }
            else
            {
                Debug.LogWarning($"Vector3 parameter '{key}' not found.");
            }
        }

        public BlockType Type => definitionType;

        public Parametors Parametors { get ; set ; }
    }

}
