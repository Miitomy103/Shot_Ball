using UnityEngine;

namespace ShotBall.Data
{
    /// <summary>
    /// Parameter‚ðSO‚Å‚Ü‚Æ‚Ü‚é
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Block Definitions")]
    public  class Parameters : ScriptableObject
    {
        public BlockDefinitionSO[] blockDefinitions;
    }



    public interface IParameters
    {
        Parameters Parameters { get; set; }
    }
}
