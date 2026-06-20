using ShotBall.Data;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ParametersBehaviour : MonoBehaviour
    {
        [SerializeField] Parameters parameters;

        public Parameters Parameters => parameters;

        static ParametersBehaviour instance;

        public ParametersBehaviour Instance => instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
