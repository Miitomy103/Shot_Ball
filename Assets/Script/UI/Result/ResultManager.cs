using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.InGame.UI;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージクリア時のリザルト画面を表示し、コイン取得状況の反映や次ステージ・タイトルへの遷移を行うクラス。
    /// </summary>
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] GameObject result;
        [SerializeField] Image[] Coins = new Image[3];
        [SerializeField] Text stageText;


        /// <summary>
        /// リザルト画面を表示し、取得済みコインとステージ名を反映する。
        /// </summary>
        public void Result()
        {
            IGet[] coins = GameLoop.Coins;
            result.SetActive(true);

            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i].IsGet)
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
        /// <summary>
        /// リザルト画面を閉じ、コイン表示をリセットしてステージをやり直す。
        /// </summary>
        public void GameReset()
        {
            result.gameObject.SetActive(false);
            GameLoop.Instance.GameReset();
            foreach (var coin in Coins)
            {
                coin.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// コイン取得状況を保存し、次のステージへ遷移する。
        /// </summary>
        public void NextStage()
        {
            SaveCoin();
            SceneControl.NextScene(SceneControl.NowStage());
        }
        /// <summary>
        /// コイン取得状況を保存し、タイトル画面(ステージ選択表示)へ戻る。
        /// </summary>
        public void BackToTitle()
        {
            Time.timeScale = 1;
            SaveCoin();
            StaticData.isSelectView = true;
            SceneControl.TitleScene();
        }
        /// <summary>
        /// このステージで取得したコインの状況を保存する。
        /// </summary>
        void SaveCoin()
        {
            IGet[] coins = GameLoop.Coins;
            Debug.Log("Save Coins");
            DataSave.CoinSave(SceneControl.StageInt(SceneControl.NowStage()), coins);
        }
    }
}
