using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.InGame
{
    public static class SceneControl
    {
        public static int nowStage { get; private set; }
        public static int nowSubStage { get; private set; }

        const string TITLENAME = "Title";

        public readonly static int maxStage = 5;
        public static void NextScene(string nowScene)
        {
            string[] stageNumbers = nowScene.Split("-");

            int stage = 0;
            int subStage = 0;
                if ( stageNumbers.Length == 2 && int.TryParse(stageNumbers[0], out stage) && int.TryParse(stageNumbers[1], out subStage))
                {
                    if (subStage >= maxStage)
                    {
                        stage++;
                        subStage = 0;
                    }
                    else
                    {
                        subStage++;
                    }
                    string stageName = stage + "-" + subStage;
                    SceneManager.LoadScene(stageName);
                    nowStage = stage;
                    nowSubStage = subStage;
                }
                else
                {
                    TitleScene();
                }
        }
        public static void StageChange(int stage,int subStage)
        {
            string stageName = stage + "-" + subStage;
            SceneManager.LoadScene(stageName);
        }
        public static string NowStage() => SceneManager.GetActiveScene().name;
        public static void TitleScene()
        {
            SceneManager.LoadScene(TITLENAME);
        }
    }
}
