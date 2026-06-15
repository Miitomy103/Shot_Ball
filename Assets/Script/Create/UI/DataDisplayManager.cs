using ShotBall.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShotBall.Create
{
    /// <summary>
    /// Editモードで作成したデータの一覧を表示するクラス
    /// </summary>
    public class DataDisplayManager : MonoBehaviour
    {
        DataDisplay[] dataDisplays;

        [SerializeField] bool isNewMark = false;

        (string, SaveData)[] dataList;
        private void Awake()
        {
            dataDisplays = GetComponentsInChildren<DataDisplay>();
        }
        private void Start()
        {
            OpenMenu();
        }

        /// <summary>
        /// ステージデータを読み込んでUIに表示する
        /// </summary>
        public void OpenMenu()
        {
            dataList = StageDataLoader.LoadAllStages(Application.persistentDataPath).ToArray();

            for (int i=0;i<dataDisplays.Length;i++)
            {
                if(i< dataList.Length)
                {
                    Texture2D texture = SaveManager.ChangeDataImage(dataList[i].Item2.imageBase64);
                    dataDisplays[i].Display(texture, dataList[i].Item1, dataList[i].Item2.playerJson);
                }
                else
                {
                    dataDisplays[i].NullDisplay();
                }
            }
            for (int i = 0; i < dataDisplays.Length; i++)
            {
                int index = i; // ← ここでコピー
                dataDisplays[index].Button.onClick.AddListener(() => dataDisplays[index].OnClick());
            }

        }

        /// <summary>
        /// ステージを選択したときの処理（外部から呼ぶ）
        /// </summary>
        public void OnClick(int index)
        {
            Debug.Log(index);
            for (int i=0;i<dataList.Length; i++)
            {
                if (i == index)
                {
                    LoadScene.BlockDataWrapper = JsonUtility.FromJson<BlockDataWrapper>(dataList[i].Item2.playerJson);
                    SceneManager.LoadScene("CreateScene");
                    
                }
            }
        }
    }
}
