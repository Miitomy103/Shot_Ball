using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public struct SceneData
    {
        public readonly static int maxStage = 5;
        public string NextScene(string nowScene)
        {
            string[] stageNumbers = nowScene.Split("-");

            int stage = 0;
            int subStage = 0;

            if (stageNumbers.Length == 2 &&int.TryParse(stageNumbers[0], out stage) &&int.TryParse(stageNumbers[1], out subStage))
            {
                if(subStage>maxStage)
                {
                    stage++;
                    subStage = 0;
                }
                else
                {
                    subStage++;
                }
            }
            return $"{stage}-{subStage}";
            
        }
    }
}
