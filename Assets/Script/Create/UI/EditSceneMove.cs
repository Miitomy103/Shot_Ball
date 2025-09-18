using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class EditSceneMove : MonoBehaviour
    {
        [SerializeField] StageUIsManager stageUIsManager;


        [SerializeField]bool isAllClear;

        [SerializeField] GameObject lockObj;

        private void Awake()
        {
            isAllClear = stageUIsManager.IsAllClear();
        }
        private void Start()
        {
            lockObj.SetActive(!isAllClear);
        }
        public void MoveEditScene()
        {
            if (isAllClear)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("SaveData");
            }
            else
            {
                LogDisplay.Instance.SetLog("ステージを全てクリアすればプレイできます");
            }
        }
    }
}
