using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// 現在のステージ名(StageName.name)をテキストに表示するクラス。
    /// </summary>
    public class StageNameUI : MonoBehaviour
    {
        [SerializeField] Text Text;

        private void Start()
        {
            Text.text = StageName.name;
        }
    }
}
