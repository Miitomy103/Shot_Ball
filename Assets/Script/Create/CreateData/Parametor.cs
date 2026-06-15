using ShotBall.Create;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Block Definition")]
    public class BlockDefinitionSO : ScriptableObject
    {
        public BlockType type;
        public bool isRotationEnabled = false;
        public ParameterData<bool> isRotation = new ParameterData<bool>("IsRotation", false);
        public List<ParameterData<float>> floatParameters = new List<ParameterData<float>>();
        public List<ParameterData<int>> intParameters = new List<ParameterData<int>>();
        public List<ParameterData<bool>> boolParameters = new List<ParameterData<bool>>();
        public List<ParameterData<Vector2>> vector2Parameters = new List<ParameterData<Vector2>>();
        public List<ParameterData<Vector3>> vector3Parameters = new List<ParameterData<Vector3>>();
        public List<ParameterData<ViewType>> uniqueViews = new List<ParameterData<ViewType>>();
    }

    [Serializable]
    public class ParameterData<T>
    {
        public string key;
        public T defaultValue;
        public ParameterData(string key, T defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }
    }
}
