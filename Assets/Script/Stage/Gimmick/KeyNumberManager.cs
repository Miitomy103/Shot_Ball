using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
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
        //public void LoadKeyNumber(KeyNumberData data)
        //{
        //    keyNumbers = new AKeyNumber[data.KeyLength];
        //    Debug.Log($"KeyNumberManager LoadKeyNumber: {data.KeyLength}");
        //    for (int i = 0; i < data.KeyLength; i++)
        //    {
        //        keyNumbers[i] = new AKeyNumber(); // ← これを追加！
        //        keyNumbers[i].LoadKeyNumber(data.akeys[i]);
        //    }
        //}

        //public KeyNumberData GetKeyNumberData()
        //{
        //    int keyLength = keyNumbers.Length;
        //    AkeyNumberData[] akeys = new AkeyNumberData[keyLength];
        //    for (int i = 0; i < keyLength; i++)
        //    {
        //        akeys[i] = keyNumbers[i].GetKeyNumberData();
        //    }
        //    return new KeyNumberData(keyLength, akeys);
        //}
    }

    [System.Serializable]
    public class AKeyNumber
    {
        //[SerializeField] private Color color = Color.white;
        //[SerializeField] public List<LaunchPad> launchPads = new List<LaunchPad>();
        //[SerializeField] public List<CrearArea> crearAreas = new List<CrearArea>();

        //public int Index { get; private set; } = 0;
        //public void AssignKeyNumber(int number)
        //{
        //    Index = number;
        //    foreach (var launch in launchPads)
        //    {
        //        launch.KeyNumber = number;
        //        launch.ColorChange(color);
        //    }
        //    foreach (var area in crearAreas)
        //    {
        //        area.KeyNumber = number;
        //        area.ColorChange(color);
        //    }
        //}
        //public void LoadKeyNumber(AkeyNumberData data)
        //{
        //    color = data.color;
        //}
        //public AkeyNumberData GetKeyNumberData()
        //{
        //    int launchLength = launchPads.Count;
        //    int creaLength = crearAreas.Count;
        //    return new AkeyNumberData(color, launchLength, creaLength);
        //}
    }
    [System.Serializable]
    public struct KeyNumberData
    {
        public int KeyLength;
        public AkeyNumberData[] akeys;
        public KeyNumberData(int l, AkeyNumberData[] a)
        {
            KeyLength = l;
            akeys = a;
        }
    }
    [System.Serializable]
    public struct AkeyNumberData
    {
        public Color color;
        public int launchLength;
        public int creaLength;
        public AkeyNumberData(Color co,int l,int c)
        {
            color = co;
            launchLength = l;
            creaLength = c;
        }
    }
}
