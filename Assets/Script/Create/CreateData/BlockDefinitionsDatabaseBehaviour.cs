using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class BlockDefinitionsDatabaseBehaviour : MonoBehaviour
    {
        [SerializeField] Parameters Parametors;

        private void Awake()
        {
            BlockDefinitionsDatabase.LoadedParametors = Parametors;
        }
    }
}
