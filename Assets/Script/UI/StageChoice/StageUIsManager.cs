using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 配下のStageUI群を管理し、クリア済みステージに応じて次のステージを解放するクラス。
    /// </summary>
    public class StageUIsManager : MonoBehaviour
    {
        StageUI[] stageUIs;
        private void Awake()
        {
            // StageUIの配列を初期化
            stageUIs = GetComponentsInChildren<StageUI>();
        }
        private void Start()
        {
            SaveLoad();
        }
        /// <summary>
        /// 保存データを読み込み、各StageUIの状態とステージの解放状況を更新する。
        /// </summary>
        public void SaveLoad()
        {
            foreach (var stageUI in stageUIs)
            {
                stageUI.SaveLoad();
            }
            for(int i = 0; i < stageUIs.Length; i++)
            {
                if ( i == 0|| stageUIs[i-1].IsAllClear())
                {
                    stageUIs[i].gameObject.SetActive(true);
                }
                else
                {
                    stageUIs[i].gameObject.SetActive(false);
                }
                Debug.Log($"Stage{stageUIs[i].Stage} clearAll:{stageUIs[i].IsAllClear()}");
            }
        }
        /// <summary>
        /// 配下の全ステージがクリア済みかどうかを判定する。
        /// </summary>
        public bool IsAllClear()
        {
            stageUIs = GetComponentsInChildren<StageUI>();
            foreach (var stageUI in stageUIs)
            {
                if (!stageUI.IsAllClear()) return false;
            }
            return true;
        }
    }
}
