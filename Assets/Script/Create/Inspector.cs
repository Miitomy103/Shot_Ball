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
        private void Awake()
        {
            instance= this; 
        }
        public void Choice(string name)
        {
            if(name==string.Empty)
            {
                uiDisplay.NullDisplay();
                return;
            }
            foreach (Prefab prefab in prefabs.prefabs)
            {
                if (prefab.name == name)
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
                if (cre.Name == name)
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
                            Text text = obj.GetComponentInChildren<Text>();
                            text.text = dataType.Name;
                            RectTransform rectTransform = obj.GetComponent<RectTransform>();
                            rectTransform.anchoredPosition = new Vector3(0, initialPositionY + spaceValue * i, 0);
                        }
                    }
                    return;
                }
            }
        }
        [Serializable]
        class ValueInspector
        {
            [SerializeField] GameObject Int;
            [SerializeField] GameObject Float;
            [SerializeField] GameObject Vector2;
            [SerializeField] GameObject Vector3;
            [SerializeField] GameObject Bool;
            public GameObject GetValueInspector(CreateObjectData.DataType type)
            {
                switch (type)
                {
                    case CreateObjectData.DataType.Int:
                        return Int;
                    case CreateObjectData.DataType.Float:
                        return Float;
                    case CreateObjectData.DataType.Vector2:
                        return Vector2;
                    case CreateObjectData.DataType.Vectror3:
                        return Vector3;
                    case CreateObjectData.DataType.Bool:
                        return Bool;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }
    }
}
