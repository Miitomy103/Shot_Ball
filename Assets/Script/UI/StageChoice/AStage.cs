using ShotBall.Create;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class AStage : MonoBehaviour
    {
        [SerializeField] StageChoice stageChoice;
        [SerializeField] private RawImage Image;
        [SerializeField] private Text StageNameText;

        string stageName;
        string json;
        Texture2D texture;

        public void IsDisplay(string stageName,string json,Texture2D texture)
        {
            this.stageName = stageName;
            this.json = json;
            this.texture = texture;

            StageNameText.text = stageName;
            Image.texture = texture;
        }
    }
}
