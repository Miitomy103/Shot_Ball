using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
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

            stageText.text=stage.GetName();
        }
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
