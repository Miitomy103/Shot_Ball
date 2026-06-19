using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public struct StageName 
    {
        public int mainStage;
        public int subStage;
        public StageName(int mainStage, int subStage)
        {
            this.mainStage = mainStage;
            this.subStage = subStage;
        }
        public readonly string GetName()
        {
            return $"{mainStage}-{subStage}";
        }
    }
}
