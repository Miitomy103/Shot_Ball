using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.InGame;

namespace ShotBall.Create
{
    /// <summary>
    /// インスペクターで設定した各種パラメータからStageBlockDataを生成するクラス。
    /// </summary>
    public class SetStageBlockData : MonoBehaviour,IStageBlockData
    {
        public StageBlockData StageBlockData => stageBlockData;

        private StageBlockData stageBlockData;

        [SerializeField] ValueKey<float>[] floats;
        [SerializeField] ValueKey<int>[] ints;
        [SerializeField] ValueKey<bool>[] bools;
        [SerializeField] ValueKey<Vector2>[] vector2s;
        [SerializeField] ValueKey<Vector3>[] vector3s;
        [SerializeField] ValueKey<ViewType>[] viewTypes;

        private void Awake()
        {
            stageBlockData = new StageBlockData();
            foreach (var f in floats)
            {
                stageBlockData.SetFloatParameter(f.key.ToString(), f.value, true);
            }
            foreach (var i in ints)
            {
                stageBlockData.SetIntParameter(i.key.ToString(), i.value, true);
            }
            foreach (var b in bools)
            {
                stageBlockData.SetBoolParameter(b.key.ToString(), b.value, true);
            }
            foreach (var v2 in vector2s)
            {
                stageBlockData.SetVector2Parameter(v2.key.ToString(), v2.value, true);
            }
            foreach (var v3 in vector3s)
            {
                stageBlockData.SetVector3Parameter(v3.key.ToString(), v3.value, true);
            }
            foreach (var vt in viewTypes)
            {
                stageBlockData.SetUniqueView(vt.key.ToString(), vt.value);
            }
        }
    }

    /// <summary>
    /// インスペクターでキーと値の組を設定するための汎用クラス。
    /// </summary>
    [System.Serializable]
    public class ValueKey<T>
    {
        public string key;
        public T value;
    }
}
