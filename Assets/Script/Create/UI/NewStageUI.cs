using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class NewStageUI : MonoBehaviour
    {
        [SerializeField] InputField stageNameInput;
        public void NewStage()
        {
            (string,SaveData)[] datas = StageDataLoader.LoadAllStages(Application.persistentDataPath).ToArray();

            foreach(var data in datas)
            {
                if (data.Item1 == stageNameInput.text)
                {
                    Debug.Log("同じ名前のステージがあります");
                    return;
                }
            }
            LoadScene.BlockDataWrapper = null;
            StageName.name = stageNameInput.text;
            SceneManager.LoadScene("CreateScene");
        }
    }
}
