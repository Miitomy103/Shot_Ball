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

        [SerializeField] LaunchPad[] launchPads;

        [SerializeField] ResultManager resultManager;

        public Action StateChangeAction { get; set; }
        public Action ResetAction { get; set; }
        public Action StartAction { get; set; }

        private StageState OnceState;

        IGet[] Coins = new IGet[3];

        int goalCount;

        int coinCount;

        readonly int coinQuantity = 3;

        protected virtual void Awake()
        {
            instance = this;
        }
        protected virtual void Start()
        {
            StageState = StageState.Setting;
        }
        public void GameStart()
        {
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
            resultManager.Result(Coins);
            ChangeState(StageState.Clear);

            var ints = SceneControl.StageInt(SceneControl.NowStage());
            PlayerPrefs.SetInt($"Stage{ints.Item1}-{ints.Item2}",1);
            Debug.Log($"Stage{ints.Item1}-{ints.Item2}" + "Save");
            PlayerPrefs.Save();
        }
        public void ClearAreaCheck(Ball ball)
        {
            goalCount++;
            if(goalCount>=launchPads.Length)
            {
                GameClear();
            }
        }
        public void CoinSet(IGet[] coins)
        {
            Coins = coins;
        }
        public void GetCoin()
        {
            coinCount++;
        }
        public void ChangeState(StageState stageState)
        {
            StageState = stageState;
            StateChangeAction();
        }
    }
}