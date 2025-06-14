using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public enum OrderInLayer
    {
        // 背景系（〜19）
        NoputArea=-5,
        Area = 4,
        StillnessBackSprite = 7,

        // 固定オブジェクト（20〜39）
        NoDrag = 20,
        Edge = 24,
        Coin = 26,

        Ball = 22,

        // フレーム系（40〜59）
        FrameRangeSprite = 40,
        Frame = 41,
        DragBackSprite = 44,
        DragNow = 48,

        // UIテキストなど（60〜）
        OnOffText = 60,
        OutSprite = 64,
        ObjectValue=66,
        FrameNumber = 70,
        FrameNumberUp = 71,
        FrameNumberUpUp = 72,
    }


}
