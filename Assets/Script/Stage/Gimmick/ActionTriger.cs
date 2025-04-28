using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ActionTriger : MonoBehaviour
    {
        [SerializeField] ActionBehaviour ActionBehaviour;

        void OnAction()
        {
            ActionBehaviour.Movements();
        }
    }
}
