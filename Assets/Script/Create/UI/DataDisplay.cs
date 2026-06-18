using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// Editモードのステージ一覧画面で、ステージを選択したときに表示される処理
    /// </summary>
    public class DataDisplay : MonoBehaviour
    {
        private RawImage RawImage;
        public Button Button { get; private set; }
        private string stageName;
        private string json;
        Texture2D texture2D;

        [SerializeField] GameObject NewStageUI;

        [SerializeField] StageExplanation stageExplanation;


        [SerializeField] private float fixedHeight = 250; // 固定したい高さ

        bool isNull = false;

        private void Awake()
        {
            RawImage = GetComponentInChildren<RawImage>();
            Button = GetComponentInChildren<Button>();
        }

        public void Display(Texture2D texture2D,string stageName,string json)
        {
            float aspect = (float)texture2D.width / texture2D.height;
            int newWidth = Mathf.RoundToInt(fixedHeight * aspect);

            // RectTransformのサイズ変更
            RectTransform rt = RawImage.rectTransform;
            rt.sizeDelta = new Vector2(newWidth, fixedHeight);

            // RawImageにテクスチャをセット
            RawImage.texture = texture2D;
            this.stageName = stageName;
            this.json = json;
            this.texture2D = texture2D;

            RawImage.color = Color.white;
        }
        public void NullDisplay()
        {
            isNull = true;
            GetComponent<INullDisplay>().Display();
        }
        public void OnClick()
        {
            if (isNull)
            {
                NewStageUI.SetActive(true);
                return;
            }

            stageExplanation.Display(stageName, json,texture2D);

        }

    }
}
