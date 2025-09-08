using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ParamatorSet : MonoBehaviour
    {
        [SerializeField] Transform parent;
        [SerializeField]Parametors Parametors; // パラメータの設定を保持するクラス
        private void Awake()
        {
            if (parent == null)
            {
                parent = transform;
            }
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent(out IParametors parametorsComponent))
                {
                    parametorsComponent.Parametors = Parametors;
                }
            }
        }
    }

    public interface IParametors
    {
        Parametors Parametors { get; set; }
    }
}
