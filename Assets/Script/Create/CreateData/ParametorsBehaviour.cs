using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ParametorsBehaviour : MonoBehaviour
    {
        [SerializeField] Parametors parametors;

        public Parametors Parametors => parametors;

        static ParametorsBehaviour instance;

        public ParametorsBehaviour Instance => instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
