using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// 1フレーム分のデータ(フレーム番号とその中に含まれるブロックデータ)を保持するクラス。
    /// </summary>
    public class FrameData
    {
        public int number = 0;

        public StageBlockData[] stageBlockDatas; // フレーム内のブロックデータ
    }
}
