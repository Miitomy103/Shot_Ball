using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Tap : MonoBehaviour
    {
        private static Tap instance;
        public static Tap Instance => instance;

        AudioSource audioSource;


        private void Awake()
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        public void Play()
        {
            audioSource.Play();
        }
    }
}