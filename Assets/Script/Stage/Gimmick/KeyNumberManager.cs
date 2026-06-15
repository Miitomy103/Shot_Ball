using ShotBall.InGame;
using UnityEngine;

namespace ShotBall.Data
{
    public class KeyNumberManager : MonoBehaviour
    {
        [SerializeField] KeyNumberColor keyNumberColor;
        public Color KeyColor(int keyNumber)
        {
            if (keyNumber < 0 || keyNumber >= keyNumberColor.colors.Length)
            {
                Debug.LogError($"KeyNumber {keyNumber} is out of range.");
                return Color.white; // デフォルトの色を返す
            }
            return keyNumberColor.colors[keyNumber];
        }
    }
    [System.Serializable]
    public struct KeyNumberDataList
    {
        public int KeyLength;
        public KeyNumberData[] aKeys;
        public KeyNumberDataList(int l, KeyNumberData[] a)
        {
            KeyLength = l;
            aKeys = a;
        }
    }
    [System.Serializable]
    public struct KeyNumberData
    {
        public Color color;
        public int launchLength;
        public int clearLength;
        public KeyNumberData(Color co,int l,int c)
        {
            color = co;
            launchLength = l;
            clearLength = c;
        }
    }
}
