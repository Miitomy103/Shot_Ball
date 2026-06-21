using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージのスクリーンショット一覧を保持し、ステージ名から対応する画像を検索するクラス。
    /// </summary>
    public class StagePictures : MonoBehaviour
    {
        const string filePath = "AreaScreenshot.";

        [SerializeField]Sprite[] stageSprites;
        /// <summary>
        /// 指定したステージ名に対応するスクリーンショットを検索する。見つからない場合はnullを返す。
        /// </summary>
        public Sprite FindSpriteByName(StageName stage)
        {
            string targetName = $"{filePath}{stage.GetName()}";
            foreach (var sprite in stageSprites)
            {
                if (sprite != null && sprite.name == targetName)
                {
                    return sprite;
                }
            }
            return null;
        }
    }
}
