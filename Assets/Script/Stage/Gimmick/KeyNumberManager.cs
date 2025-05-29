using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class KeyNumberManager : MonoBehaviour
    {
        [SerializeField] private AKeyNumber[] keyNumbers;

        private void Start()
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
            if (launchPads.Length < crearAreas.Length) Debug.LogError("okashiiiii");
            IKeyNumber[] keyNumbers = new IKeyNumber[launchPads.Length + crearAreas.Length];
            foreach (var launch in launchPads)
            {
                keyNumbers[index++] = launch;
            }
            foreach (var area in crearAreas)
            {
                keyNumbers[index++] = area;
            }

            foreach(var key in keyNumbers)
            {
                key.KeyNumber = number;
                key.ColorChange(color);
            }

        }
    }

}
