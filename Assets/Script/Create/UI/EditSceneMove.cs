using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 全ステージクリア時にのみ編集(Create)シーンへの遷移を許可するクラス。
    /// </summary>
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
        /// <summary>
        /// 全ステージクリア済みなら編集シーンへ移動する。未クリアならログを表示する。
        /// </summary>
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
