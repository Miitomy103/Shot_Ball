using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// 複数のUILineInserterの表示/非表示を一括で切り替えるクラス。
    /// </summary>
    public class Lines : MonoBehaviour
    {
        [SerializeField] UILineInserter[] lineInserter;

        /// <summary>
        /// 管理している線挿入UIをまとめて表示/非表示にする。
        /// </summary>
        public void ActiveSelf(bool isActive)
        {
            for (int i = 0; i < lineInserter.Length; i++)
            {
                lineInserter[i].gameObject.SetActive(isActive);
            }
        }
    }
}
