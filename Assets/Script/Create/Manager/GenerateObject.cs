using ShotBall.Data;
using ShotBall.InGame;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// BlockTypeに応じてブロックのプレハブを生成・初期化するクラス。
    /// </summary>
    public class GenerateObject : MonoBehaviour
    {
        [SerializeField] PrefabLists prefabLists;
        [SerializeField] Parameters parameters; // パラメータの設定を保持するクラス

        [SerializeField] Objects objects;


        int blockIdCount = 0;

        /// <summary>
        /// 指定したBlockTypeのオブジェクトを新規生成する。
        /// </summary>
        public CreateObject Generate(BlockType blockType)
        {
            foreach(var p in prefabLists.prefabs)
            {
                if (p.type == blockType)
                {
                    GameObject obj = Instantiate(p.PrefabObj, Vector3.zero+new Vector3(0,0,-5), Quaternion.identity);
                    obj.transform.parent = transform;
                    CreateObject createObject = obj.GetComponent<CreateObject>();
                    createObject.BlockId = blockIdCount;
                    blockIdCount++;
                    ParameterSet(obj); // パラメータを設定
                    objects.Add(createObject); 
                    HandleFollower.Instance.SpriteSet(createObject.GetComponent<SpriteRenderer>(), createObject.IsSizeChange);
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
        /// <summary>
        /// 保存されたStageBlockDataからオブジェクトを復元生成する。
        /// </summary>
        public CreateObject LoadObject(StageBlockData data)
        {
            if (prefabLists == null) { Debug.LogError("GenerateObject: prefabLists がInspectorに割り当てられていません"); return null; }
            foreach (var p in prefabLists.prefabs)
            {
                if (p.type == data.Type)
                {
                    GameObject obj = Instantiate(p.PrefabObj, Vector3.zero + new Vector3(0, 0, -5), Quaternion.identity);
                    obj.transform.parent = transform;
                    CreateObject createObject = obj.GetComponent<CreateObject>();
                    createObject.Load(data);
                    createObject.BlockId = data.GetIntParameter("BlockId");
                    ParameterSet(obj); // パラメータを設定
                    objects.Add(createObject);
                    return createObject;
                }
            }
            return null; // 生成できるオブジェクトが見つからない場合はnullを返す
        }
        void ParameterSet(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IParameters>(out var parameters))
            {
                parameters.Parameters = this.parameters;
            }
            if(gameObject.TryGetComponent<IStageBlockData>(out var stageBlockData))
            {
            }
        }
    }
}
