using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.InGame.UI;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace ShotBall.InGame
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] GameObject result;
        [SerializeField] Image[] Coins = new Image[3];
        [SerializeField] Text stageText;


        public void Result()
        {
            IGet[] coins = GameLoop.Coins;
            result.SetActive(true);

            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i].Get)
                {
                    Coins[i].gameObject.SetActive(true);
                }
            }

            string sceneName = SceneControl.NowStage();

            if(TryGetComponent<IName>(out var nameComponent))
            {
                sceneName = nameComponent.Name;
            }

            stageText.text = sceneName;
        }
        public void GameReset()
        {
            result.gameObject.SetActive(false);
            GameLoop.Instance.GameReset();
            foreach (var coin in Coins)
            {
                coin.gameObject.SetActive(false);
            }
        }
        public void NextStage()
        {
            SaveCoin();
            SceneControl.NextScene(SceneControl.NowStage());
        }
        public void BackToTitle()
        {
            SaveCoin();
            SceneControl.TitleScene();
        }
        void SaveCoin()
        {
            IGet[] coins = GameLoop.Coins;
            Debug.Log("Save Coins");
            DataSave.CoinSave(SceneControl.StageInt(SceneControl.NowStage()), coins);
        }
    }
}
