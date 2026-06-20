using System.Collections.Generic;

/// <summary>
/// サウンドを管理するクラス
/// </summary>
public static class SoundController
{
    private static Dictionary<string, IPlay> soundDictionary = new Dictionary<string, IPlay>();

    /// <summary>
    /// サウンドを再生する
    /// </summary>
    public static void PlaySound(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            soundDictionary[soundName].Play();
        }
    }

    /// <summary>
    /// サウンドを登録する
    /// </summary>
    public static void RegisterSound(string soundName, IPlay sound)
    {
        if (!soundDictionary.ContainsKey(soundName))
        {
            soundDictionary.Add(soundName, sound);
        }
    }
    /// <summary>
    /// サウンドを登録解除する
    /// </summary>
    public static void UnregisterSound(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            soundDictionary.Remove(soundName);
        }
    }
}
