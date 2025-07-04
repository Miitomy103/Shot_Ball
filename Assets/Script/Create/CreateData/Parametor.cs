using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Block Definition")]
    public class BlockDefinition : ScriptableObject
    {
        public BlockType type;
        public ParametorVector2 position= new ParametorVector2("Position", Vector2.zero);
        public ParametorVector2 size = new ParametorVector2("Size", Vector2.one);
        public ParametorVector3 rotation = new ParametorVector3("Rotation", Vector3.zero);
        public bool isRotationEnabled = false;
        public ParametorBool isRotation = new ParametorBool("IsRotation", false);
        public List<ParameterFloat> floatParameters = new List<ParameterFloat>();
        public List<ParametorInt> intParameters = new List<ParametorInt>();
        public List<ParametorBool> boolParameters = new List<ParametorBool>();
        public List<ParametorVector2> vector2Parameters = new List<ParametorVector2>();
        public List<ParametorVector3> vector3Parameters = new List<ParametorVector3>();
    }

    [Serializable]
    public class ParameterFloat
    {
        public string key;
        public float defaultValue;
        public ParameterFloat(string key, float defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }
    }
    [Serializable]
    public class ParametorInt
    {
        public string key;
        public int defaultValue;
        public ParametorInt(string key, int defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }
    }
    [Serializable]
    public class ParametorBool
    {
        public string key;
        public bool defaultValue;
        public ParametorBool(string key, bool defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }
    }
    [Serializable]
    public class ParametorVector2
    {
        public string key;
        public Vector2 defaultValue;
        public ParametorVector2(string key, Vector2 defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }
    }
    [Serializable]
    public class ParametorVector3
    {
        public string key;
        public Vector3 defaultValue;
        public ParametorVector3(string key, Vector3 defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }
    }
}
