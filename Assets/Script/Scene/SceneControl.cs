using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    /// <summary>
    /// シーン名("メイン-サブ"形式)の解析とシーン遷移を行う静的クラス。
    /// </summary>
    public static class SceneControl
    {

        const string TITLENAME = "Title";

        public readonly static int maxStage = 5;
        /// <summary>
        /// 現在のシーン名から次のサブステージ（または次のメインステージ／タイトル）へ遷移する。
        /// </summary>
        public static void NextScene(string nowScene)
        {
            string[] stageNumbers = nowScene.Split("-");

            int stage = 0;
            int subStage = 0;
            if (stageNumbers.Length == 2 && int.TryParse(stageNumbers[0], out stage) && int.TryParse(stageNumbers[1], out subStage))
            {
                if (subStage >= maxStage)
                {
                    if (!DataSave.StageAllClear(stage, maxStage))
                    {
                        TitleScene();
                        return;
                    }
                    stage++;
                    subStage = 1;
                }
                else
                {
                    subStage++;
                }
                if (stage >= maxStage)
                {
                    TitleScene();
                    return;
                }
                string stageName = stage + "-" + subStage;
                SceneManager.LoadScene(stageName);
            }
            else
            {
                TitleScene();
            }
        }
        /// <summary>
        /// 指定したステージのシーンへ遷移する。
        /// </summary>
        public static void StageChange(StageName stage)
        {
            string stageName = stage.mainStage + "-" + stage.subStage;
            SceneManager.LoadScene(stageName);
        }
        /// <summary>
        /// 現在アクティブなシーン名を取得する。
        /// </summary>
        public static string NowStage() => SceneManager.GetActiveScene().name;
        /// <summary>
        /// "メイン-サブ"形式のシーン名をStageNameに変換する。
        /// </summary>
        public static StageName StageInt(string name)
        {
            string[] stageNumbers = name.Split("-");
            if (int.TryParse(stageNumbers[0], out int stage) && int.TryParse(stageNumbers[1], out int subStage))
            {
                return new StageName(stage, subStage);
            }
            return new(0, 0);
        }
        /// <summary>
        /// タイトルシーンへ遷移する。
        /// </summary>
        public static void TitleScene()
        {
            StaticData.isSelectView = true;
            SceneManager.LoadScene(TITLENAME);
        }
    }
}
