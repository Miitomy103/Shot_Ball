using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.Data;
using ShotBall.InGame;
using System;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class Inspector : MonoBehaviour
    {
        static Inspector instance;
        public static Inspector Instance => instance;

        [SerializeField] PrefabDatas prefabs;
        [SerializeField] CreateObjectData createObjectData;
        [SerializeField] UIDisplay uiDisplay;
        [SerializeField] ValueInspector valueInspector;
        [SerializeField] Transform valueUIParent;

        [Header("Texts")]
        [SerializeField] Text gimmickText;
        [SerializeField] Text uniqueText;

        List<GameObject> valueUIList = new List<GameObject>();

        readonly float initialPositionY = -300f;
        readonly float spaceValue = -150;

        [SerializeField,ReadOnly] StageBlockData stageBlockData; // í«â¡: StageBlockDataÇÃéQè∆
        private void Awake()
        {
            instance= this; 
        }
        public void Choice(StageBlockData blockData)
        {
            if(blockData==null)
            {
                uiDisplay.NullDisplay();
                return;
            }
            foreach (Prefab prefab in prefabs.prefabs)
            {
                if (prefab.name == blockData.Type.ToString())
                {
                    uiDisplay.GenerateDisplay(prefab.PrefabObj);
                }
            }
            foreach(var v in valueUIList)
            {
                Destroy(v);
            }
            valueUIList.Clear();
            foreach (var cre in createObjectData.GimmickDatasArray)
            {
                if (cre.Name == blockData.Type.ToString())
                {
                    gimmickText.text = cre.Name;
                    uniqueText.text = cre.Unique;
                    for (int i = 0; i < cre.dataTypes.Length; i++)
                    {
                        var dataType = cre.dataTypes[i];
                        GameObject valueInspectorObj = valueInspector.GetValueInspector(dataType.Type);
                        if (valueInspectorObj != null)
                        {
                            GameObject obj = Instantiate(valueInspectorObj, valueUIParent);
                            valueUIList.Add(obj);
                            IChangeText text = obj.GetComponentInChildren<IChangeText>();
                            text.ChangeText(dataType.Name);
                            RectTransform rectTransform = obj.GetComponent<RectTransform>();
                            rectTransform.anchoredPosition = new Vector3(0, initialPositionY + spaceValue * i, 0);
                        }
                    }
                    int count = 0;
                    foreach (var f in blockData.floatParametors.Keys)
                    {
                        GameObject obj = GenerateValue(count, DataType.Float, f);
                        if (obj != null)
                        {
                            IChangeText text = obj.GetComponentInChildren<IChangeText>();
                            text.ChangeText(f);

                            count++;
                        }
                    }

                }
            }
        }
        GameObject GenerateValue(int valueNumber,DataType type,string key)
        {
            GameObject valueInspectorObj = valueInspector.GetValueInspector(type);
            if (valueInspectorObj != null)
            {
                GameObject obj = Instantiate(valueInspectorObj, valueUIParent);
                valueUIList.Add(obj);
                IChangeText text = obj.GetComponentInChildren<IChangeText>();
                text.ChangeText(key);
                RectTransform rectTransform = obj.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector3(0, initialPositionY + spaceValue * valueNumber, 0);
                return obj;
            }
            return null;
        }
        [Serializable]
        class ValueInspector
        {
            [SerializeField] GameObject Int;
            [SerializeField] GameObject Float;
            [SerializeField] GameObject Vector2;
            [SerializeField] GameObject Vector3;
            [SerializeField] GameObject Bool;
            public GameObject GetValueInspector(DataType type)
            {
                switch (type)
                {
                    case DataType.Int:
                        return Int;
                    case DataType.Float:
                        return Float;
                    case DataType.Vector2:
                        return Vector2;
                    case DataType.Vectror3:
                        return Vector3;
                    case DataType.Bool:
                        return Bool;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }
    }
}
