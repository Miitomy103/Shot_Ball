using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ShotBall.InGame
{
    public class StageUI : MonoBehaviour
    {
        [SerializeField] int stage;
        [SerializeField] Color baseColor;
        [SerializeField] Color clearColor;
        [SerializeField] Image backImage;
        [SerializeField] Text text;
        [SerializeField] Image[] stages = new Image[5];
        private void OnValidate()
        {
            Inialize();
        }
        private void Inialize()
        {
            backImage.color = baseColor;
            text.text = $"Stage{stage}";
            for (int i = 0; i < stages.Length; i++)
            {
                bool clear = (1 == PlayerPrefs.GetInt($"Stage{stage}-{i+1}", 0));
                if (clear) stages[i].color = clearColor;
                else stages[i].color = Color.white;
            }
        }
        private void Start()
        {
            Inialize();

            for (int i = 0; i < stages.Length; i++)
            {
                int index = i+1; // ローカル変数にコピー
                Button button = stages[i].GetComponent<Button>();
                button.onClick.AddListener(() => OnClick(index));
            }

        }
        void OnClick(int index)
        {
            SceneControl.StageChange(stage,index);
        }
        public void TestClearColor()
        {
            for (int i = 0; i < stages.Length; i++)
            {
                bool clear = (i==2);
                if (clear) stages[i].color = clearColor;
                else stages[i].color = Color.white;
            }
        }
    }
}
