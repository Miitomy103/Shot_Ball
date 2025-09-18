using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Rotate : MonoBehaviour
    {
        private static Rotate instance;
        public static Rotate Instance => instance;

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
