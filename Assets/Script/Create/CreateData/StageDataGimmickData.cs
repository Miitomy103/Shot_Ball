using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// StageBlockDataをセーブ用の文字列に変換するための静的クラス。
    /// </summary>
    public static class StageDataGimmickData
    {
        /// <summary>
        /// BlockTypeに応じて必要なパラメータを取り出し、セーブ用の文字列に変換する。
        /// </summary>
        public static string GetGimmickData(StageBlockData stageBlockData)
        {
            switch(stageBlockData.Type)
            {
                case BlockType.AntiGravityArea:
                    return string.Empty;
                case BlockType.AutoRotator:
                    return stageBlockData.floatParameters.TryGetValue("Speed", out float rotationSpeed) ? rotationSpeed.ToString() : "0";
                case BlockType.BallLauncher:
                    Vector2 launchDirection = stageBlockData.vector2Parameters.TryGetValue("LaunchDirection", out Vector2 direction) ? direction : Vector2.zero;
                    string returnString = (stageBlockData.floatParameters.TryGetValue("LaunchPower", out float launchPower) ? launchPower.ToString() : "0") + "," +launchDirection.x + "," + launchDirection.y;
                    return returnString;
                case BlockType.Block:
                    return string.Empty;
                case BlockType.Bouncy:
                    return stageBlockData.GetFloatParameter("BounceForce").ToString();
                case BlockType.CreaArea:
                    return stageBlockData.GetIntParameter("KeyNumber").ToString();
                case BlockType.Coin:
                    return string.Empty;
                case BlockType.DieBlock:
                    return string.Empty;
                case BlockType.GravitySwitch:
                    return stageBlockData.GetBoolParameter("StartAntiGravity").ToString();
                case BlockType.LaunchPad:
                    return stageBlockData.GetBoolParameter("IsAntiGravity").ToString() +","+ stageBlockData.GetIntParameter("KeyNumber").ToString();
                case BlockType.NoPutArea:
                    return string.Empty;
                case BlockType.OnOffBlock:
                    return stageBlockData.GetBoolParameter("StartIsOn").ToString();
                case BlockType.SwitchBlock:
                    return string.Empty;
                case BlockType.TheWorld:
                    return stageBlockData.GetFloatParameter("Time").ToString();
                case BlockType.Warp:
                    return string.Empty;
                default:
                    //Debug.LogError("Unknown BlockType: " + stageBlockData.BlockType);
                    return null;
            }
        }
    }
}
