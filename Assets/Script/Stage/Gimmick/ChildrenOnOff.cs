using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ChildrenOnOff : MonoBehaviour, IOnOff
    {
        
        bool isOn;
        public bool IsOn => isOn;

        public Transform Transform => transform;


        OnOff[] onOffs;

        private void Awake()
        {
            OnOff[] o = GetComponentsInChildren<OnOff>();
            onOffs = o.Where(t => t != null && t.gameObject != gameObject).ToArray();


            foreach (var i in onOffs) Debug.Log(i.transform.name);
        }


        public void ItOff()
        {
            isOn = false;
            foreach (var i in onOffs) i.ItOff();
        }

        public void ItOn()
        {
            isOn = true;
            foreach (var i in onOffs) i.ItOn();
        }
    }
}
