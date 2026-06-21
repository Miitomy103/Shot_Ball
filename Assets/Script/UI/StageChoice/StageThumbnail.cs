using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    /// <summary>
    /// ステージ選択画面のサムネイル表示。ステージの画像・コイン取得状況・ステージ名を表示する。
    /// </summary>
    public class StageThumbnail : MonoBehaviour
    {
        StagePictures stagePictures;
        [SerializeField] Image stageImage;
        RectTransform stageImageRectTransform;

        [SerializeField] GameObject[] Coins=new GameObject[3];
        [SerializeField] Text stageText;
        private void Awake()
        {
            stagePictures = GetComponent<StagePictures>();
            stageImageRectTransform = stageImage.GetComponent<RectTransform>();
        }
        private void Start()
        {
            StageExit();
        }
        /// <summary>
        /// 指定したステージの画像・コイン取得状況・ステージ名をサムネイルに反映する。
        /// </summary>
        public void StageChange(StageName stage)
        {
            Sprite sprite = stagePictures.FindSpriteByName(stage);

            Vector2 size = new Vector2(sprite.textureRect.width, sprite.textureRect.height);

            stageImage.sprite = sprite;
            stageImageRectTransform.sizeDelta = size;

            for (int i=0;i<3;i++)
            {
                if(DataSave.CoinGet(stage, i))
                {
                    Coins[i].SetActive(true);
                }
                else
                {
                    Coins[i].SetActive(false);
                }
            }
            if (stage.mainStage == 0)
            {
                stageText.text = "Tutorial";
                return;
            }
            stageText.text=stage.GetName();
        }
        /// <summary>
        /// サムネイルを未選択状態(画像なし)に戻す。
        /// </summary>
        public void StageExit()
        {
            stageImage.sprite = null;
            stageImageRectTransform.sizeDelta = Vector2.zero;
            for (int i = 0; i < Coins.Length; i++)
            {
                Coins[i].SetActive(false);
            }
            stageText.text = "-";
        }
    }
}
