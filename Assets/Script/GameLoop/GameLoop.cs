using ShotBall.InGame;
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

        IGet[] Coins = new IGet[3];

        int goalCount;

        int coinCount;

        readonly int coinQuantity = 3;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            StageState = StageState.Setting;
        }
        public void GameStart()
        {
            foreach (var l in launchPads) l.GameStart();
            ChangeState(StageState.Playing);
        }
        public void GameReset()
        {
            foreach (var l in launchPads) l.GameReset();
            ChangeState(StageState.Setting);
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
        }
    }
}