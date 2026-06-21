using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// シーン名("メイン-サブ"形式)から次のシーン名を計算する構造体。SceneControlと同種の機能を持つ。
    /// </summary>
    public struct SceneData
    {
        public readonly static int maxStage = 5;
        /// <summary>
        /// 現在のシーン名から次のシーン名("メイン-サブ"形式)を計算して返す。
        /// </summary>
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
