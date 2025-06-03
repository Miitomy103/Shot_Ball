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

        public DragObject DragObject => throw new System.NotImplementedException();

        IOnOff[] onOffs;

        private void Awake()
        {
            onOffs = GetComponentsInChildren<MonoBehaviour>(true)
                .OfType<IOnOff>()
                .Where(x => x != (object)this)
                .ToArray();

            foreach (var i in onOffs) Debug.Log(i.Transform.name);
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
