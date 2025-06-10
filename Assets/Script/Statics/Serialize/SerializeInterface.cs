using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{

    [Serializable]
    public class SerializeInterface<TInterface>
    {
        [SerializeField]
        private GameObject m_gameobject;

        private TInterface m_interface;

        public TInterface Interface
        {
            get
            {
                if (m_interface == null)
                {
                    m_interface = m_gameobject.GetComponent<TInterface>();
                }
                return m_interface;
            }
        }
    }

}
