using ShotBall.Data;
using ShotBall.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// Editモードでの、オブジェクトのステータスを変更するためのUIクラス
    /// </summary>
    public class Inspector : MonoBehaviour
    {
        static Inspector instance;
        public static Inspector Instance => instance;

        [SerializeField] PrefabLists prefabs;
        [SerializeField] CreateObjectData createObjectData;
        [SerializeField] UIDisplay uiDisplay;
        [SerializeField] ValueInspector valueInspector;
        [SerializeField] Transform valueUIParent;

        [Header("Texts")]
        [SerializeField] Transform gimmickText;
        [SerializeField] Transform uniqueText;
        private IChangeText gimmickTextComponent;
        private IChangeText uniqueTextComponent;

        readonly List<GameObject> valueUIList = new List<GameObject>();

        readonly float initialPositionY = -300f;
        readonly float spaceValue = -150;

        private void Awake()
        {
            instance= this; 
            gimmickTextComponent = gimmickText.GetComponentInChildren<IChangeText>();
            uniqueTextComponent = uniqueText.GetComponentInChildren<IChangeText>();
        }
        public void Choice(StageBlockData blockData)
        {

            foreach (var cre in createObjectData.GimmickDatasArray)
            {
                if (cre.Name == blockData.Type)
                {
                    ValueSet(blockData, cre.Name.ToString(), cre.Unique);
                }
            }
        }

        public void ValueSet(StageBlockData blockData,string name,string unique)
        {
            if (blockData == null)
            {
                uiDisplay.NullDisplay();
                return;
            }
            foreach (Prefab prefab in prefabs.prefabs)
            {
                if (prefab.type == blockData.Type)
                {
                    uiDisplay.GenerateDisplay(prefab.PrefabObj);
                }
            }
            foreach (var v in valueUIList)
            {
                Destroy(v);
            }
            valueUIList.Clear();
            Debug.Log("Choice");
            Debug.Log("名前が一緒");
            gimmickTextComponent.ChangeText(name);
            uniqueTextComponent.ChangeText(unique);
            int count = 0;
            foreach (var f in blockData.floatParameters.Keys.ToList())
            {
                if (IsHideInspector(f, blockData)) continue;
                DataType type = GetViewType(f, blockData.uniqueViews, DataType.Float);

                GameObject obj = GenerateValue(count, type, f);
                if (obj != null)
                {
                    ISetValue<float> setValue = obj.GetComponentInChildren<ISetValue<float>>();
                    setValue.Setup(f, blockData.SetFloatParameter);
                    setValue.SetValue(blockData.floatParameters[f]);

                    count++;
                }
            }

            foreach (var i in blockData.intParameters.Keys.ToList())
            {
                if (IsHideInspector(i, blockData)) continue;
                DataType type = GetViewType(i, blockData.uniqueViews, DataType.Int);
                GameObject obj = GenerateValue(count, type, i);
                if (obj != null)
                {
                    ISetValue<int> setValue = obj.GetComponentInChildren<ISetValue<int>>();
                    setValue.Setup(i, blockData.SetIntParameter);
                    setValue.SetValue(blockData.intParameters[i]);
                    count++;
                }
            }
            foreach (var b in blockData.boolParameters.Keys.ToList())
            {
                if (IsHideInspector(b, blockData)) continue;
                DataType type = GetViewType(b, blockData.uniqueViews, DataType.Bool);
                GameObject obj = GenerateValue(count, type, b);
                if (obj != null)
                {
                    ISetValue<bool> setValue = obj.GetComponentInChildren<ISetValue<bool>>();
                    setValue.Setup(b, blockData.SetBoolParameter);
                    setValue.SetValue(blockData.boolParameters[b]);
                    count++;
                }
            }
            foreach (var v2 in blockData.vector2Parameters.Keys.ToList())
            {
                if (IsHideInspector(v2, blockData)) continue;
                DataType type = GetViewType(v2, blockData.uniqueViews, DataType.Vector2);
                Debug.Log($"v2: {v2}, Type: {type}"); // デバッグログを追加
                GameObject obj = GenerateValue(count, type, v2);

                if (obj != null)
                {
                    ISetValue<Vector2> setValue = obj.GetComponentInChildren<ISetValue<Vector2>>();
                    setValue.Setup(v2, blockData.SetVector2Parameter);
                    setValue.SetValue(blockData.vector2Parameters[v2]);
                    count++;
                }
            }
            foreach (var v3 in blockData.vector3Parameters.Keys.ToList())
            {
                if (IsHideInspector(v3, blockData)) continue;
                DataType type = GetViewType(v3, blockData.uniqueViews, DataType.Vectror3);
                GameObject obj = GenerateValue(count, type, v3);
                if (obj != null)
                {
                    ISetValue<Vector3> setValue = obj.GetComponentInChildren<ISetValue<Vector3>>();
                    setValue.Setup(v3, blockData.SetVector3Parameter);
                    setValue.SetValue(blockData.vector3Parameters[v3]);
                    count++;
                }
            }
        }

        private bool IsHideInspector(string key,StageBlockData stageBlockData)
        {
            string[] keys = new string[]
            {
                "Position",
                "Rotatison",
                "Size",
                "KeyNumber",
                "BlockId",
            };

            foreach(var s in keys)
            {
                if (s == key) return true;
            }

            if (stageBlockData.isDisplays.TryGetValue(key, out bool isDisplay))
            {
                if(!isDisplay)return true;
            }
            return false;
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
                Debug.Log($"GenerateValue: {valueNumber}, Type: {type}, Key: {key}");
                return obj;
            }
            return null;
        }
        DataType GetViewType(string key,Dictionary<string,ViewType> dic,DataType none)
        {
            if(dic.ContainsKey(key))
            {
                ViewType view = dic[key];
                switch (view)
                {
                    case ViewType.Arrow:
                        return DataType.Arrow;
                    default:
                        return none;
                }
            }
            else
            {
                Debug.LogWarning($"ViewType for key '{key}' not found.");
                return none;
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
            [SerializeField] GameObject Arrow;
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
                    case DataType.Arrow:
                        return Arrow;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }
    }
}
