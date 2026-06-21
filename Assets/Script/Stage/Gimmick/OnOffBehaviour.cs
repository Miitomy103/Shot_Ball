using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 同じオブジェクトのIOnOff実装へItOn/ItOff呼び出しを中継するクラス。
    /// OnOffBlockが接続先オブジェクトを統一的に操作するために使う。
    /// </summary>
    public class OnOffBehaviour : MonoBehaviour
    {
        IOnOff onOff;
        private void Awake()
        {
            onOff = GetComponent<IOnOff>();
            if (onOff == null)
            {
                Debug.LogError("IOnOffインターフェースが実装されていません。");
            }
        }
        /// <summary>
        /// 接続先をオン状態にする。
        /// </summary>
        public void ItOn()
        {
            if (onOff != null)
            {
                onOff.ItOn();
            }
        }
        /// <summary>
        /// 接続先をオフ状態にする。
        /// </summary>
        public void ItOff()
        {
            if (onOff != null)
            {
                onOff.ItOff();
            }
        }
    }
}
