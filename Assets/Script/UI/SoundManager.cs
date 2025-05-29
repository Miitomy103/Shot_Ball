using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

namespace ShotBall.InGame.Audio
{
    public class SoundManager : MonoBehaviour
    {
        [Header("オーディオミキサー")]
        [SerializeField] AudioMixer mixer;

        const string VolumeName = "Volume";

        private void Start()
        {
            float dB = PlayerPrefs.GetFloat(VolumeName, 0);
            mixer.SetFloat("Master", dB);
        }

        // ボリューム調整（dB値） -80（無音）〜 0（最大）
        public void OnMasterSliderChanged(float value)
        {
            float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
            mixer.SetFloat("Master", dB);
            PlayerPrefs.SetFloat(VolumeName, dB);
        }
    }
}
