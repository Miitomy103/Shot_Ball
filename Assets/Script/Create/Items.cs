using ShotBall.Create;
using ShotBall.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class Items : MonoBehaviour
    {
        RectTransform[] rectTransforms;
        [SerializeField] PrefabDatas prefabDatas;

        [SerializeField] Button up;
        [SerializeField] Button down;

        [SerializeField] GenerateObject generateObject;
        ItemData[] itemDatas;
        int frameCount => rectTransforms.Length;

        Button[] buttons;

        int page = 0;

        struct ItemData
        {
            public GameObject[] items;
            public BlockType[] types;
            public ItemData(GameObject[] i, BlockType[] t)
            {
                items = i;
                types = t;
            }
            public void ActiveChange(bool active)
            {
                foreach (var item in items)
                {
                    if (item == null) continue; // nullチェック
                    item.SetActive(active);
                }
            }
        }

        void Awake()
        {
            // 自身を除外
            rectTransforms = Array.FindAll(GetComponentsInChildren<RectTransform>(), rt => rt != transform);
            buttons = Array.FindAll(GetComponentsInChildren<Button>(), b => b != up && b != down);
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i; // ローカル変数を使用してクロージャーの問題を回避
                buttons[i].onClick.AddListener(() => ItemClick(index));
            }
        }

        private void Start()
        {
            int totalPrefabs = prefabDatas.prefabs.Length;
            int totalPages = Mathf.CeilToInt((float)totalPrefabs / frameCount);
            itemDatas = new ItemData[totalPages];

            for (int i = 0; i < totalPages; i++)
            {
                GameObject[] items = new GameObject[frameCount];
                BlockType[] types = new BlockType[frameCount]; // Prefabの種類を格納する配列
                for (int j = 0; j < frameCount; j++)
                {
                    int index = i * frameCount + j;
                    if (index >= totalPrefabs) break;

                    var prefab = prefabDatas.prefabs[index];
                    items[j] = Instantiate(prefab.PrefabObj, rectTransforms[j].position, Quaternion.identity, rectTransforms[j]);
                    items[j].SetActive(false);
                    types[j] = prefab.type; // Prefabの種類を取得
                }
                itemDatas[i] = new ItemData(items,types);
            }

            itemDatas[0].ActiveChange(true);

            up.onClick.AddListener(() => OnClick(-1));
            down.onClick.AddListener(() => OnClick(+1));
        }
        private void OnClick(int index)
        {
            if(page + index < 0 || page + index >= itemDatas.Length) return;
            itemDatas[page].ActiveChange(false);
            page += index;
            itemDatas[page].ActiveChange(true);
            ClickSound.Instance.Play();
        }
        private void ItemClick(int index)
        {
            generateObject.Generate(itemDatas[page].types[index]);
        }
    }

}
