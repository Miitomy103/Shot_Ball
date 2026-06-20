using TMPro;
using UnityEngine;

namespace ShotBall.Create
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
