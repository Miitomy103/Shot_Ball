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