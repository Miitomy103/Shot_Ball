using ShotBall.InGame;
using UnityEngine;

namespace ShotBall.Data
{
    /// <summary>
    /// キーナンバーに対応する色を管理するクラス。
    /// </summary>
    public class KeyNumberManager : MonoBehaviour
    {
        [SerializeField] KeyNumberColor keyNumberColor;
        /// <summary>
        /// キーナンバーに対応する色を返す。範囲外の場合は白を返す。
        /// </summary>
        public Color KeyColor(int keyNumber)
        {
            if (keyNumber < 0 || keyNumber >= keyNumberColor.colors.Length)
            {
                Debug.LogError($"KeyNumber {keyNumber} is out of range.");
                return Color.white; // �f�t�H���g�̐F��Ԃ�
            }
            return keyNumberColor.colors[keyNumber];
        }
    }
    /// <summary>
    /// キーナンバーのデータリスト。
    /// </summary>
    [System.Serializable]
    public struct KeyNumberDataList
    {
        /// <summary>
        /// キーの総数。
        /// </summary>
        public int KeyLength;

        /// <summary>
        /// 各キーのデータ配列。
        /// </summary>
        public KeyNumberData[] aKeys;
        public KeyNumberDataList(int l, KeyNumberData[] a)
        {
            KeyLength = l;
            aKeys = a;
        }
    }
    /// <summary>
    /// 1つのキーナンバーに対応するデータ。
    /// </summary>
    [System.Serializable]
    public struct KeyNumberData
    {
        /// <summary>
        /// このキーに対応する色。
        /// </summary>
        public Color color;

        /// <summary>
        /// このキーで発射されたボールの数。
        /// </summary>
        public int launchLength;

        /// <summary>
        /// このキーのクリアに必要なボールの数。
        /// </summary>
        public int clearLength;
        public KeyNumberData(Color co,int l,int c)
        {
            color = co;
            launchLength = l;
            clearLength = c;
        }
    }
}
