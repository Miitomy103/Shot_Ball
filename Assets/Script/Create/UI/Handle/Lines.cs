using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    public class Lines : MonoBehaviour
    {
        [SerializeField] UILineInserter[] lineInserter;

        public void ActiveSelf(bool isActive)
        {
            for (int i = 0; i < lineInserter.Length; i++)
            {
                lineInserter[i].gameObject.SetActive(isActive);
            }
        }
    }
}
