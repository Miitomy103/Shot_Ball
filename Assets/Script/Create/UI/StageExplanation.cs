using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class StageExplanation : MonoBehaviour
    {
        [SerializeField] GameObject UI;
        [SerializeField] RawImage Thumbnail;
        [SerializeField] Text StageName;
        [SerializeField] ShareManager shareManager;
        string stageName;
        string json;
        Texture2D thumbnail;

        [SerializeField] private float fixedHeight = 250; // 固定したい高さ
        public void Display(string stageName,string json,Texture2D texture2D)
        {
            this.stageName = stageName;
            this.json = json;
            this.thumbnail = texture2D;

            float aspect = (float)texture2D.width / texture2D.height;
            int newWidth = Mathf.RoundToInt(fixedHeight * aspect);

            // RectTransformのサイズ変更
            RectTransform rt = Thumbnail.rectTransform;
            rt.sizeDelta = new Vector2(newWidth, fixedHeight);

            // RawImageにテクスチャをセット
            Thumbnail.texture = texture2D;
            StageName.text = stageName;
            UI.SetActive(true);
        }

        public void Close()
        {
            UI.SetActive(false);
        }
        public void EditStage()
        {
            LoadScene.BlockDataWrapper = JsonUtility.FromJson<BlockDataWrapper>(json);
            StageName.name = stageName;
            SceneManager.LoadScene("CreateScene");
        }
        public void DeleteStage()
        {
            Debug.Log("Delete Stage: " + stageName);
            SaveManager.Delete(stageName);
        }
        public void PlayStage()
        {
            LoadScene.BlockDataWrapper = JsonUtility.FromJson<BlockDataWrapper>(json);
            Create.StageName.name = stageName;
            SceneManager.LoadScene("InGameScene");
        }

        public void ShareOpen()
        {
            shareManager.OpenWindow(new SaveData(json, SaveManager.TextureToBase64(thumbnail)), stageName);
        }
    }
}
