using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
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
