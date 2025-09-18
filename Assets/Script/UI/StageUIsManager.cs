using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class StageUIsManager : MonoBehaviour
    {
        [SerializeField]StageUI[] stageUIs;
        private void Awake()
        {
            // StageUI‚Ì”z—ñ‚ğ‰Šú‰»
            stageUIs = GetComponentsInChildren<StageUI>();
        }
        private void Start()
        {
            SaveLoad();
        }
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
