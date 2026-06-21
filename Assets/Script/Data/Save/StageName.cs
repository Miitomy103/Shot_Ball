using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// メインステージ番号とサブステージ番号の組でステージを識別する構造体。
    /// </summary>
    public struct StageName
    {
        public int mainStage;
        public int subStage;
        public StageName(int mainStage, int subStage)
        {
            this.mainStage = mainStage;
            this.subStage = subStage;
        }
        /// <summary>
        /// "メインステージ-サブステージ" 形式の名前文字列を返す。
        /// </summary>
        public readonly string GetName()
        {
            return $"{mainStage}-{subStage}";
        }
    }
}
