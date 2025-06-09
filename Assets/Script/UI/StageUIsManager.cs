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
        public void SaveLoad()
        {
            foreach (var stageUI in stageUIs)
            {
                stageUI.SaveLoad();
            }
        }
    }
}
