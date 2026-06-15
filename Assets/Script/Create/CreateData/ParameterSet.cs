using UnityEngine;

namespace ShotBall.InGame
{
    public class ParameterSet : MonoBehaviour
    {
        [SerializeField] Transform parent;
        [SerializeField] Parameters Parameters; // パラメータの設定を保持するクラス
        private void Awake()
        {
            if (parent == null)
            {
                parent = transform;
            }
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent(out IParameters parametersComponent))
                {
                    parametersComponent.Parameters = Parameters;
                }
            }
        }
    }

    public interface IParameters
    {
        Parameters Parameters { get; set; }
    }
}
