using ShotBall.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// Edit��ʏ㕔�̃I�u�W�F�N�g���Ǘ����邽�߂�UI
    /// </summary>
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
                    if (item == null) continue; // null�`�F�b�N
                    item.SetActive(active);
                }
            }
        }

        void Awake()
        {
            // ���g�����O
            rectTransforms = Array.FindAll(GetComponentsInChildren<RectTransform>(), rt => rt != transform);
            buttons = Array.FindAll(GetComponentsInChildren<Button>(), b => b != up && b != down);
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i; // ���[�J���ϐ����g�p���ăN���[�W���[�̖������
                buttons[i].onClick.AddListener(() => ItemClick(index));
            }
        }

        private void Start()
        {
            if (prefabDatas == null) { Debug.LogError("Items: prefabDatas がInspectorに割り当てられていません"); return; }
            int totalPrefabs = prefabDatas.prefabs.Length;
            int totalPages = Mathf.CeilToInt((float)totalPrefabs / frameCount);
            itemDatas = new ItemData[totalPages];

            for (int i = 0; i < totalPages; i++)
            {
                GameObject[] items = new GameObject[frameCount];
                BlockType[] types = new BlockType[frameCount]; // Prefab�̎�ނ��i�[����z��
                for (int j = 0; j < frameCount; j++)
                {
                    int index = i * frameCount + j;
                    if (index >= totalPrefabs) break;

                    var prefab = prefabDatas.prefabs[index];
                    items[j] = Instantiate(prefab.PrefabObj, rectTransforms[j].position, Quaternion.identity, rectTransforms[j]);
                    items[j].SetActive(false);
                    types[j] = prefab.type; // Prefab�̎�ނ��擾
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
            SoundController.PlaySound("Click");
        }
        private void ItemClick(int index)
        {
            generateObject.Generate(itemDatas[page].types[index]);
        }
    }

}
