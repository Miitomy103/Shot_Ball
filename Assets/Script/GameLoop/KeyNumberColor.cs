using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// キーナンバーに対応する色の配列を保持する ScriptableObject。
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "KeyNumberColor", menuName = "ScriptableObjects/KeyNumberColor", order =1)]
    public class KeyNumberColor : ScriptableObject
    {
        public Color[] colors;
    }
}
