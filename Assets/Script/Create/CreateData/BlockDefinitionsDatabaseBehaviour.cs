using ShotBall.Data;
using UnityEngine;

namespace ShotBall.InGame
{
    public class BlockDefinitionsDatabaseBehaviour : MonoBehaviour
    {
        [SerializeField] Parameters Parameters;

        private void Awake()
        {
            BlockDefinitionsDatabase.LoadedParameters = Parameters;
        }
    }
}
