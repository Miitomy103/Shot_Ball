using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージ共有時にJSONへ変換して送信する、ステージ名と暗号化済みセーブデータの組。
    /// </summary>
    [System.Serializable]
    public class ShareData
    {
        public string stageName;
        public string encryptedJson;
        public ShareData(string stageName, string encryptedJson)
        {
            this.stageName = stageName;
            this.encryptedJson = encryptedJson;
        }
    }
}
