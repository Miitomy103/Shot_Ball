using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Audio
{
    [RequireComponent(typeof(AudioSource))]
    /// <summary>
    /// タップ（ドラッグ開始など）操作時の効果音を再生するシングルトンクラス。
    /// </summary>
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