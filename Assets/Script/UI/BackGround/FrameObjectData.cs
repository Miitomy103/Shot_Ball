using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 配置可能な残数を管理するクラス。残数が0になると生成不可になる。
    /// </summary>
    public class FrameObjectData : MonoBehaviour
    {
        [SerializeField] bool infinity;
        [SerializeField] int objectCount = 1;

        private GameObject FrameObject;
        public int FieldObjectCount { get; private set; }
        private void Awake()
        {
            FieldObjectCount = objectCount;
        }
        public void Inialize(GameObject gameObject)
        {
            FrameObject = gameObject;
        }
        /// <summary>
        /// 残数を1減らし、複製元のオブジェクトを返す。残数が無い場合はnullを返す。
        /// </summary>
        public GameObject Instantiate()
        {
            if (FieldObjectCount <= 0) return null;

            FieldObjectCount--;
            return FrameObject;
        }
        /// <summary>
        /// オブジェクトが枠に戻されたときに残数を1増やす。
        /// </summary>
        public void PutAway()
        {
            FieldObjectCount++;
        }
    }
}
