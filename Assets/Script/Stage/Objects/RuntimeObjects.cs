using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class RuntimeObjects : MonoBehaviour
    {
        public GameObject[] objects;

        private void Awake()
        {
            for(int i=0;i<transform.childCount;i++)
            {
                objects[i] = transform.GetChild(i).gameObject;
            }
        }
        private void Start()
        {
            Outline outline = GetComponent<Outline>();

            foreach(var g in objects)
            {
                Outline o=  g.AddComponent<Outline>();
                o = outline;
            }
        }
    }
}
