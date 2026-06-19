using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージの進行状況を保存するクラス
    /// </summary>
    public static class DataSave
    {
        public static void StageSave(StageName stage,bool isClear)
        {
            PlayerPrefs.SetInt($"Stage{stage.mainStage}-{stage.subStage}", isClear ? 1 : 0);
            PlayerPrefs.Save();
        }
        public static void StageReset(StageName stage)
        {
            PlayerPrefs.SetInt($"Stage{stage.mainStage}-{stage.subStage}", 0);
            PlayerPrefs.Save();
        }
        public static bool StageGet(StageName stage)
        {
            return PlayerPrefs.GetInt($"Stage{stage.mainStage}-{stage.subStage}", 0) == 1;
        }
        public static bool StageAllClear(int mainStage, int maxSubStage)
        {
            for (int i = 1; i <= maxSubStage; i++)
            {
                if (!StageGet(new StageName(mainStage, i))) return false;
            }
            return true;
        }
        public static void CoinSave(StageName stage, IGet[] gets)
        {
            for (int i = 0; i < gets.Length; i++)
            {
                if (!gets[i].IsGet) continue;
                bool isGet = PlayerPrefs.GetInt($"Coin{stage.mainStage}-{stage.subStage}-{i}", 0) == 1;
                if (!isGet)
                {
                    PlayerPrefs.SetInt($"Coin{stage.mainStage}-{stage.subStage}-{i}", 1);
                }
            }
            Debug.Log($"CoinSave: Stage {stage.mainStage}-{stage.subStage} Coins Saved");
            PlayerPrefs.Save();
        }
        public static bool CoinGet(StageName stage, int index)
        {
            return PlayerPrefs.GetInt($"Coin{stage.mainStage}-{stage.subStage}-{index}", 0) == 1;
        }
        public static void CoinReset(StageName stage)
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetInt($"Coin{stage.mainStage}-{stage.subStage}-{i}", 0);
            }
            PlayerPrefs.Save();
        }
    }
}
