using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
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
        public GameObject Instantiate()
        {
            if (FieldObjectCount <= 0) return null;

            FieldObjectCount--;
            return FrameObject;
        }
        public void PutAway()
        {
            FieldObjectCount++;
        }
    }
}
