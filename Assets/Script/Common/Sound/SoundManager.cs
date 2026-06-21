using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace ShotBall.InGame.Audio
{
    /// <summary>
    /// マスター音量のスライダーUIとAudioMixerを連動させ、設定をPlayerPrefsに保存するクラス。
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        [Header("オーディオミキサー")]
        [SerializeField] AudioMixer mixer;
        [SerializeField] Slider masterSlider;

        const string VolumeName = "Volume";

        private void Awake()
        {
            if (masterSlider != null)
            {
                masterSlider.onValueChanged.AddListener(OnMasterSliderChanged);
            }
        }

        private void Start()
        {
            float dB = PlayerPrefs.GetFloat(VolumeName, 0);
            mixer.SetFloat("Master", dB);

            float linear = Mathf.Pow(10f, dB / 20f);
            masterSlider.SetValueWithoutNotify(linear);
        }

        /// <summary>
        /// スライダーの値(0〜1)をdBに変換してマスター音量に反映し、保存する。
        /// </summary>
        public void OnMasterSliderChanged(float value)
        {
            float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
            mixer.SetFloat("Master", dB);
            PlayerPrefs.SetFloat(VolumeName, dB);
            Debug.Log("OnValueChanged");
        }

        /// <summary>
        /// dB値を指定してマスター音量を設定し、保存する。
        /// </summary>
        public void SetVolume(float dB)
        {
            mixer.SetFloat("Master", dB);
            PlayerPrefs.SetFloat(VolumeName, dB);

            float linear = Mathf.Pow(10f, dB / 20f);
            masterSlider.SetValueWithoutNotify(linear);
        }
    }
}
