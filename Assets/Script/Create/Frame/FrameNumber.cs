using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    public class FrameNumber : MonoBehaviour,IChangeText
    {
        [SerializeField] TextMeshPro text;
        public void ChangeText(string str)
        {
            text.text = str;
        }
    }
}
