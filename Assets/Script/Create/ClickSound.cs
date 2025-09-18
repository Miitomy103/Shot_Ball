using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ClickSound : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;

        public static ClickSound Instance=>instance;
        static ClickSound instance;


        private void Awake()
        {
            instance = this;
        }
        public void Play()
        {
            audioSource.Play();
        }
    }
}
