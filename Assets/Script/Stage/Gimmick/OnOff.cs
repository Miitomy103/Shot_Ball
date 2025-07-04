using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class OnOff : MonoBehaviour
    {
        IOnOff onOff;
        private void Awake()
        {
            onOff = GetComponent<IOnOff>();
            if (onOff == null)
            {
                Debug.LogError("IOnOffインターフェースが実装されていません。");
            }
        }
        public void ItOn()
        {
            if (onOff != null)
            {
                onOff.ItOn();
            }
        }
        public void ItOff()
        {
            if (onOff != null)
            {
                onOff.ItOff();
            }
        }
    }
}
