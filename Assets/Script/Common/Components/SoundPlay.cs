using UnityEngine;
public class SoundPlay : MonoBehaviour,IPlay
{
    [SerializeField] string soundName = "Click";
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        SoundController.RegisterSound(soundName, this);
    }
    void OnDestroy()
    {
        SoundController.UnregisterSound(soundName);
    }
    public void Play()
    {
        audioSource.Play();
    }
}
