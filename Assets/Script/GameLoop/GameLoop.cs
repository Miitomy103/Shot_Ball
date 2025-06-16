using ShotBall.InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class GameLoop : MonoBehaviour
    {
        static GameLoop instance;
        public static GameLoop Instance => instance;
        public static StageState StageState { get; private set; }

        [SerializeField]public List<LaunchPad> launchPads = new List<LaunchPad>();

        [SerializeField] ResultManager resultManager;

        public Action StateChangeAction { get; set; }
        public Action ResetAction { get; set; }
        public Action StartAction { get; set; }

        private StageState OnceState;

        public static IGet[] Coins { get; set; } = new IGet[3];

        [SerializeField] int goalCount;

        protected virtual void Awake()
        {
            instance = this;
            Time.timeScale = 1;
        }
        protected virtual void Start()
        {
            ChangeState(StageState.Setting);
        }
        public void GameStart()
        {
            if (StageState == StageState.Playing) return;
            ChangeState(StageState.Playing);
            StartAction();
        }
        public void GameReset()
        {
            ChangeState(StageState.Setting);
            goalCount = 0;
            Debug.Log("GameReset");
            ResetAction();
        }
        public void OnMenu()
        {
            Time.timeScale = 0;
            OnceState = StageState;
            ChangeState(StageState.Pose);
        }
        public void OffMenu()
        {
            Time.timeScale = 1;
            ChangeState(OnceState);
        }
        void GameClear()
        {
            resultManager.Result();
            ChangeState(StageState.Clear);

            StageName s = SceneControl.StageInt(SceneControl.NowStage());
            DataSave.StageSave(s, true);
        }
        public void ClearAreaCheck(Ball ball)
        {
            goalCount++;
            if(goalCount>=launchPads.Count)
            {
                GameClear();
            }
        }
        public void CoinSet(IGet[] coins)
        {
            Coins = coins;
        }
        public void ChangeState(StageState stageState)
        {
            StageState = stageState;
            StateChangeAction?.Invoke();
        }
    }
}