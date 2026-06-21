using ShotBall.Data;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// インスペクターで設定したParametersをBlockDefinitionsDatabase.LoadedParametersに登録するクラス。
    /// </summary>
    public class BlockDefinitionsDatabaseBehaviour : MonoBehaviour
    {
        [SerializeField] Parameters Parameters;

        private void Awake()
        {
            BlockDefinitionsDatabase.LoadedParameters = Parameters;
        }
    }
}
