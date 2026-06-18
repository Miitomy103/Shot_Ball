using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 子オブジェクトにOnOffをつけると、親オブジェクトのOnOffで一括でOnOffできるようになるクラス
    /// </summary>
    public class ChildrenOnOff : MonoBehaviour, IOnOff
    {
        
        bool isOn;
        public bool IsOn => isOn;

        public Transform Transform => transform;


        OnOffBehaviour[] onOffs;

        private void Awake()
        {
            OnOffBehaviour[] o = GetComponentsInChildren<OnOffBehaviour>();
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
