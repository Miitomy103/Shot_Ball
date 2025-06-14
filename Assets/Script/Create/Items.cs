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
        ItemData[] itemDatas;
        int frameCount => rectTransforms.Length;

        int page = 0;

        struct ItemData
        {
            GameObject[] items;
            public ItemData(GameObject[] i)
            {
                items = i;
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
        }

        private void Start()
        {
            int totalPrefabs = prefabDatas.prefabs.Length;
            int totalPages = Mathf.CeilToInt((float)totalPrefabs / frameCount);
            itemDatas = new ItemData[totalPages];

            for (int i = 0; i < totalPages; i++)
            {
                GameObject[] items = new GameObject[frameCount];
                for (int j = 0; j < frameCount; j++)
                {
                    int index = i * frameCount + j;
                    if (index >= totalPrefabs) break;

                    var prefab = prefabDatas.prefabs[index];
                    items[j] = Instantiate(prefab.PrefabObj, rectTransforms[j].position, Quaternion.identity, rectTransforms[j]);
                    items[j].SetActive(false);
                }
                itemDatas[i] = new ItemData(items);
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
        }
    }

}
