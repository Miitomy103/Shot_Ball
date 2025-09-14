using ShotBall.Data;
using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    public class GenerateObject : MonoBehaviour
    {
        [SerializeField] PrefabDatas prefabDatas;
        [SerializeField]Parametors parametors; // パラメータの設定を保持するクラス

        [SerializeField] Objects objects;


        int blockIdCount = 0;

        public CreateObject Generate(BlockType blockType)
        {
            foreach(var p in prefabDatas.prefabs)
            {
                if (p.type == blockType)
                {
                    GameObject obj = Instantiate(p.PrefabObj, Vector3.zero+new Vector3(0,0,-5), Quaternion.identity);
                    obj.transform.parent = transform;
                    CreateObject createObject = obj.GetComponent<CreateObject>();
                    createObject.BlockId = blockIdCount;
                    blockIdCount++;
                    ParametorSet(obj); // パラメータを設定
                    objects.Add(createObject);
                    if (createObject != null)
                    {
                        createObject.StageBlockData.SetType(blockType);
                        return createObject;
                    }
                    else
                    {
                        Debug.LogError("CreateObject component not found on the prefab.");
                        Destroy(obj);
                        return null;
                    }
                }
            }
            return null; // 生成できるオブジェクトが見つからない場合はnullを返す
        }
        public CreateObject LoadObject(StageBlockData data)
        {
            foreach (var p in prefabDatas.prefabs)
            {
                if (p.type == data.Type)
                {
                    GameObject obj = Instantiate(p.PrefabObj, Vector3.zero + new Vector3(0, 0, -5), Quaternion.identity);
                    obj.transform.parent = transform;
                    CreateObject createObject = obj.GetComponent<CreateObject>();
                    createObject.Load(data);
                    createObject.BlockId = data.GetIntParameter("BlockId");
                    ParametorSet(obj); // パラメータを設定
                    objects.Add(createObject);
                    return createObject;
                }
            }
            return null; // 生成できるオブジェクトが見つからない場合はnullを返す
        }
        void ParametorSet(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IParametors>(out var parametors))
            {
                parametors.Parametors = this.parametors;
            }
            if(gameObject.TryGetComponent<IStageBlockData>(out var stageBlockData))
            {
                //stageBlockData.Parametors = this.parametors;
                //stageBlockData.StageBlockData.blockId = blockId++;
            }
        }
    }
}
