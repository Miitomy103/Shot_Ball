using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 子オブジェクトすべてにObjectsNumberを付与し、インデックス番号を割り振るクラス。
    /// </summary>
    public class ObjectsNumbers : MonoBehaviour
    {
        public static GameObject[] Childs { get; private set; }
        private void Awake()
        {
            Childs=gameObject.GetComponentsInChildren<GameObject>();
            for(int i = 0; i < Childs.Length; i++)
            {
                if (Childs[i].GetComponent<ObjectsNumber>() == null)
                {
                    Childs[i].AddComponent<ObjectsNumber>();
                }
                Childs[i].GetComponent<ObjectsNumber>().number = i;
            }
        }
    }
}
