using ShotBall.InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class NewStageUI : MonoBehaviour
    {
        [SerializeField] InputField stageNameInput;

        [SerializeField] Text StagePath;
        public void NewStage()
        {
            (string,SaveData)[] datas = StageDataLoader.LoadAllStages(Application.persistentDataPath).ToArray();

            foreach(var data in datas)
            {
                if (data.Item1 == stageNameInput.text)
                {
                    Debug.Log("同じ名前のステージがあります");
                    LogDisplay.Instance.SetLog("同じ名前のステージがあります");
                    return;
                }
                if(string.IsNullOrEmpty(stageNameInput.text))
                {
                    Debug.Log("ステージ名を入力してください");
                    LogDisplay.Instance.SetLog("ステージ名を入力してください");
                    return;
                }
            }
            LoadScene.BlockDataWrapper = null;
            StageName.name = stageNameInput.text;
            SceneManager.LoadScene("CreateScene");
        }

        public void SceneLoad()
        {
            StartCoroutine(Enumerator());
        }

        IEnumerator Enumerator()
        {
            string shareKey = StagePath.text;

            string json = string.Empty;
            bool hasError = false;

            yield return JsonBin.Download(
                shareKey,
                jsonString =>
                {
                    json = jsonString;
                },
                errorCode =>
                {
                    hasError = true;
                    Debug.Log("Error: " + errorCode);
                    LogDisplay.Instance.SetLog("コードが読み取れません");
                }
            );

            // ダウンロード完了後にエラーチェック
            if (hasError)
                yield break;

            ShareData shareData = JsonUtility.FromJson<ShareData>(json);

            try
            {
                Convert.FromBase64String(shareData.encryptedJson);
            }
            catch (FormatException)
            {
                Debug.LogError("渡された文字列はBase64ではありません: " + shareData.encryptedJson);
                yield break;
            }


            string data = SecureSave.DecryptFromString(shareData.encryptedJson);



            LoadScene.BlockDataWrapper= JsonUtility.FromJson<BlockDataWrapper>(data);

            string n = shareData.stageName;

            // 入力が空ならエラーにする
            if (string.IsNullOrEmpty(stageNameInput.text))
            {
                Debug.Log("ステージ名を入力してください");
                yield break;
            }

            // 既存のステージ一覧を取得
            (string, SaveData)[] datas = StageDataLoader.LoadAllStages(Application.persistentDataPath).ToArray();

            // かぶっていたら数字を付けて新しい名前を探す
            string baseName = n;
            int i = 1;
            while (datas.Any(d => d.Item1 == n))
            {
                n = baseName + i;
                i++;
            }

            Debug.Log("決定したステージ名: " + n);


            StageName.name = n;

            SceneManager.LoadScene("CreateScene");

            // 成功時の処理
            Debug.Log("成功: " + json);
        }


    }
}
