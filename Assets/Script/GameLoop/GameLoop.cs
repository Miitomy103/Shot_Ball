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

        public event Action StartAction;
        public event Action ResetAction;
        public event Action StateChangeAction;

        private StageState OnceState;

        List<Ball> clearBalls = new List<Ball>();

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

            launchPads.Clear();
            ObjectBase[] objectBases= DragObjects.Instance.GetAllObjects();
            foreach (var obj in objectBases)
            {
                if(obj.TryGetComponent<LaunchPad>(out var launchPad))
                {
                    launchPads.Add(launchPad);
                }
            }
        }
        /// <summary>
        /// スタートボタンを押したときの処理。
        /// </summary>
        public void GameStart()
        {
            if (StageState == StageState.Playing) return;
            ChangeState(StageState.Playing);
            StartAction?.Invoke();
        }
        /// <summary>
        /// リセットボタンを押したときの処理。
        /// </summary>
        public void GameReset()
        {
            ChangeState(StageState.Setting);
            goalCount = 0;
            Debug.Log("GameReset");
            ResetAction?.Invoke();
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
            if(clearBalls.Contains(ball)) return;
            clearBalls.Add(ball);
            Debug.Log("GameLoop ClearAreaCheck");
            goalCount++;
            if(goalCount>=launchPads.Count)
            {
                GameClear();
            }
        }
        public void CoinSet(IGet[] coins)
        {
            Debug.Log("GameLoop CoinSet");
            Coins = coins;
        }
        public void ChangeState(StageState stageState)
        {
            StageState = stageState;
            StateChangeAction?.Invoke();
        }
    }
}