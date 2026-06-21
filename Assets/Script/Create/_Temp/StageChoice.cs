using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// _Temp配下の実験用クラス。保存済みの全ステージデータを読み込む(stageImagesへの表示処理は未実装)。
    /// </summary>
    public class StageChoice : MonoBehaviour
    {
        [SerializeField] Image[] stageImages;
        private void Start()
        {
            string folder = Application.persistentDataPath; // 保存場所
            List<(string fileName, SaveData data)> allStages = StageDataLoader.LoadAllStages(folder);


        }
    }
}
