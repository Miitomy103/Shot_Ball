using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
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
