using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class KeyNumberManager : MonoBehaviour
    {
        [SerializeField] private AKeyNumber[] keyNumbers;

        private void Awake()
        {
            for (int i = 0; i < keyNumbers.Length; i++)
            {
                keyNumbers[i].AssignKeyNumber(i);
            }
        }
    }

    [System.Serializable]
    public class AKeyNumber
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private LaunchPad[] launchPads;
        [SerializeField] private CrearArea[] crearAreas;

        int index = 0;
        public void AssignKeyNumber(int number)
        {
            foreach (var launch in launchPads)
            {
                launch.KeyNumber = number;
                launch.ColorChange(color);
            }
            foreach (var area in crearAreas)
            {
                area.KeyNumber = number;
                area.ColorChange(color);
            }

        }
    }

}
