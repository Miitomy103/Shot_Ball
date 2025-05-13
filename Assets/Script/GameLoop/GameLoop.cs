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
            ResetAction();
        }
        void GameClear()
        {
            resultManager.Result(Coins);
            ChangeState(StageState.Clear);
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