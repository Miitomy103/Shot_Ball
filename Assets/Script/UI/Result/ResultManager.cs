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


        public void Result(IGet[] coins)
        {
            result.SetActive(true);

            for(int i=0;i<coins.Length;i++)
            {
                if (coins[i].Get)
                {
                    Coins[i].gameObject.SetActive(true);
                }
            }

            string sceneName = SceneControl.NowStage();

            stageText.text = sceneName;
        }
        public void GameReset()
        {
            result.gameObject.SetActive(false);
            GameLoop.Instance.GameReset();
        }
        public void NextStage()
        {

        }
    }
}
