using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DragObjects : MonoBehaviour
    {
        private GameObject[] objects;
        [SerializeField] Transform RuntimeParent;
        private void Awake()
        {
            objects = new GameObject[RuntimeParent.childCount];
            for(int i=0;i<RuntimeParent.childCount;i++)
            {
                objects[i] = RuntimeParent.GetChild(i).gameObject;
            }


            objects = objects.Where(obj => obj != null).ToArray();
        }

        public GameObject[] GetObjects() => objects;
    }

}
