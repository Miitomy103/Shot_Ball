using UnityEngine;
using ShotBall.Data;

namespace ShotBall.InGame
{
    /// <summary>
    /// parent配下の子オブジェクトのうちIParametersを実装するものに、設定したParametersを配布するクラス。
    /// </summary>
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
}
