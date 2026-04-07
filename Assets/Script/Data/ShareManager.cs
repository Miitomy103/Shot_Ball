using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class ShareManager : MonoBehaviour
    {
        [SerializeField]Text Text;

        string json;
        string stageName;

        [SerializeField] Text copyText;

        Action<string> onSuccess;
        Action<string> onError;

        private void Start()
        {
            onSuccess = Success;
            onError = Error;
        }
        public void OpenWindow(SaveData saveData,string stageName)
        {
            Text.text = stageName;
            json = saveData.playerJson;
            this.stageName = stageName;

            copyText.text=string.Empty;

            gameObject.SetActive(true);
        }

        public void ShareClick()
        {
            string base64=SecureSave.EncryptToString(json);
            ShareData shareData = new ShareData(stageName, base64);
            string j = JsonUtility.ToJson(shareData);
            StartCoroutine(JsonBin.Upload(j, onSuccess, onError));

        }
        void Success(string binId)
        {
            copyText.text = $"{binId}";
        }
        void Error(string error)
        {
            copyText.text = error;
        }
    }
}
