using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

namespace ShotBall.InGame
{
    public class StageUI : MonoBehaviour
    {
        [SerializeField] int stage;
        public int Stage => stage;
        [SerializeField] Color baseColor;
        [SerializeField] Color clearColor;
        [SerializeField] Image backImage;
        [SerializeField] Text text;
        [SerializeField] Image[] stages = new Image[5];
        [SerializeField] Image[] clearImages = new Image[5];
        StageThumbnail stageThumbnail;

        private void Awake()
        {
            stageThumbnail=GameObject.Find("StageWindow").GetComponent<StageThumbnail>();

            for (int i = 0; i < stages.Length; i++)
            {
                clearImages[i] = stages[i]
    .GetComponentsInChildren<Image>(true)
    .FirstOrDefault(img => img != stages[i]);
            }
            if (clearImages != null)
            {
                foreach (var clearImage in clearImages)
                {
                    if (clearImage != null) clearImage.color = baseColor;
                }
            }
        }
        private void OnValidate()
        {
            backImage.color = baseColor;

            if (text != null) text.text = $"Stage{stage}";

            if(clearImages!=null)
            {
                foreach (var clearImage in clearImages)
                {
                    if (clearImage != null) clearImage.color = baseColor;
                }
            }
        }
        private void Inialize()
        {
            backImage.color = baseColor;

            if(text!=null) text.text = $"Stage{stage}";
            SaveLoad();
        }

        public void SaveLoad()
        {
            for(int i = 0; i < stages.Length; i++)
            {
                bool clear = (1 == PlayerPrefs.GetInt($"Stage{stage}-{i + 1}", 0));
                if (clear) clearImages[i].gameObject.SetActive(true);
                else clearImages[i].gameObject.SetActive(false);
            }
        }
        private void Start()
        {
            Inialize();

            for (int i = 0; i < stages.Length; i++)
            {
                int index = i+1; // ローカル変数にコピー

                HoverHandler handler = stages[i].gameObject.GetComponent<HoverHandler>();
                if(handler == null)
                {
                    handler = stages[i].gameObject.AddComponent<HoverHandler>();
                }
                handler.onEnter = () => ButtonEnter(index );
                handler.onExit = () => ButtonExit(index );


                Button button = stages[i].GetComponent<Button>();
                button.onClick.AddListener(() => OnClick(index));
            }

        }

        public bool IsAllClear()
        {
            for (int i = 0; i < stages.Length; i++)
            {
                bool clear = (1 == PlayerPrefs.GetInt($"Stage{stage}-{i + 1}", 0));
                if (!clear) return false;
            }
            return true;
        }
        void OnClick(int index)
        {
            SceneControl.StageChange(new StageName(stage,index));
        }
        public void TestClearColor()
        {
            for (int i = 0; i < stages.Length; i++)
            {
                bool clear = (i==2);
                if (clear) clearImages[i].gameObject.SetActive(true);
                else clearImages[i].gameObject.SetActive(false);
            }
        }
        void ButtonEnter(int index)
        {
            stageThumbnail.StageChange(new StageName(stage, index));
        }
        void ButtonExit(int index)
        {
            //stageThumbnail.StageExit();
        }
    }
}
