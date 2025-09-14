using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
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

        public void Test()
        {
            if (!CanTest()) return;

            StaticData.blockDataWrapper = objects.Data();

            string json = JsonUtility.ToJson(StaticData.blockDataWrapper, true);
            string path = Path.Combine(Application.dataPath, $"StageData/StageData.テスト用.json");
            File.WriteAllText(path, json);

            allObjects = Object.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                obj.SetActive(false);
            }
            SceneManager.LoadScene("CreateTestScene",LoadSceneMode.Additive);

        }
        public void TestEnd()
        {
            SceneManager.UnloadSceneAsync("CreateTestScene");
            foreach (var obj in allObjects)
            {
                obj.SetActive(true);
            }
        }

        public bool CanTest()
        {
            if(coinCount.coinCount != CoinCount.maxCoinCount)
            {
                return false;
            }
            return true;
        }

        public void Title()
        {
            SceneManager.LoadScene("SaveData");
        }
    }
}
