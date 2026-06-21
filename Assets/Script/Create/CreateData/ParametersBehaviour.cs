using ShotBall.Data;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// インスペクターで設定したParametersを保持し、Instance経由で他から参照できるようにするクラス。
    /// </summary>
    public class ParametersBehaviour : MonoBehaviour
    {
        [SerializeField] Parameters parameters;

        public Parameters Parameters => parameters;

        static ParametersBehaviour instance;

        /// <summary>
        /// このコンポーネントのインスタンス。
        /// </summary>
        public ParametersBehaviour Instance => instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
