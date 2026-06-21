using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// Createシーン全体を管理するシングルトンクラス。テストプレイの開始/終了やタイトルへの遷移を行う。
    /// </summary>
    public class CreateManager : MonoBehaviour
    {
        static CreateManager instance;
        public static CreateManager Instance { get { return instance; } }
        [SerializeField] Button testButton;

        [SerializeField] Objects objects;
        [SerializeField] CoinCount coinCount;

        GameObject[] allObjects;

        private void Awake()
        {
            instance = this;
            testButton.onClick.AddListener(() =>Test());
        }

        /// <summary>
        /// テストボタンを押したときの処理。既存オブジェクトを非表示にし、テストプレイ用シーンを追加読み込みする。
        /// </summary>
        public void Test()
        {
            if (!CanTest()) return;

            StaticData.blockDataWrapper = objects.Data();

            allObjects = Object.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                obj.SetActive(false);
            }
            SceneManager.LoadScene("CreateTestScene",LoadSceneMode.Additive);

            SoundController.PlaySound("Click");

        }
        /// <summary>
        /// テストプレイ用シーンをアンロードし、編集中のオブジェクトを再表示する。
        /// </summary>
        public void TestEnd()
        {
            SceneManager.UnloadSceneAsync("CreateTestScene");
            foreach (var obj in allObjects)
            {
                obj.SetActive(true);
            }
        }

        /// <summary>
        /// テストプレイを開始できる状態かどうかを判定する(コインの数が3つであるかをチェック)。
        /// </summary>
        public bool CanTest()
        {
            if(!coinCount.IsCoinCountValid())
            {
                LogDisplay.Instance.SetLog("コインの数を3つにしてください");
                return false;
            }
            return true;
        }

        /// <summary>
        /// タイトル(セーブデータ選択)シーンに遷移する。
        /// </summary>
        public void Title()
        {
            SceneManager.LoadScene("SaveData");
        }
    }
}
