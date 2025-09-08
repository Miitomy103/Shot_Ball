using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "KeyNumberColor", menuName = "ScriptableObjects/KeyNumberColor", order =1)]
    public class KeyNumberColor : ScriptableObject  
    {
        public Color[] colors; // êFÇÃîzóÒ
    }
}
