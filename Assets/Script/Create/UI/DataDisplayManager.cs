using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class DataDisplayManager : MonoBehaviour
    {
        DataDisplay[] dataDisplays;

        [SerializeField] bool isNewMark = false;

        (string, SaveData)[] datas;
        private void Awake()
        {
            dataDisplays = GetComponentsInChildren<DataDisplay>();
        }
        private void Start()
        {
            OpenMenu();
        }

        public void OpenMenu()
        {
            datas = StageDataLoader.LoadAllStages(Application.persistentDataPath).ToArray();

            for (int i=0;i<dataDisplays.Length;i++)
            {
                if(i< datas.Length)
                {
                    Texture2D texture = SaveManager.ChangeDataImage(datas[i].Item2.imageBase64);
                    dataDisplays[i].Display(texture, datas[i].Item1, datas[i].Item2.playerJson);
                }
                else
                {
                    dataDisplays[i].NullDisplay();
                }
            }
            for (int i = 0; i < dataDisplays.Length; i++)
            {
                int index = i; // © ‚±‚±‚ÅƒRƒs[
                dataDisplays[index].Button.onClick.AddListener(() => dataDisplays[index].OnClick());
            }

        }
        public void OnClick(int index)
        {
            Debug.Log(index);
            for (int i=0;i<datas.Length; i++)
            {
                if (i == index)
                {
                    LoadScene.BlockDataWrapper = JsonUtility.FromJson<BlockDataWrapper>(datas[i].Item2.playerJson);
                    SceneManager.LoadScene("CreateScene");
                    
                }
            }
        }
    }
}
