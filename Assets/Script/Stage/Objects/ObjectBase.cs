using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージ上に配置されるオブジェクトの基底クラス。
    /// </summary>
    public abstract class ObjectBase : MonoBehaviour
    {
        public int BlockId { get; set; }
        /// <summary>
        /// 他のオブジェクトと重なっているかどうか。
        /// </summary>
        protected abstract bool IsOverLapping();
        /// <summary>
        /// IsOverLappingを外部から呼び出すための公開メソッド。
        /// </summary>
        public bool PublicOverLapping() => IsOverLapping();
    }
}
