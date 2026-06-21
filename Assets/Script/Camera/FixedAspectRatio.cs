using UnityEngine;

/// <summary>
/// 起動時に指定した解像度・フルスクリーン設定を適用するクラス。
/// </summary>
public class FixedAspectRatio : MonoBehaviour
{
    private const int DefaultScreenWidth = 1920;
    private const int DefaultScreenHeight = 1080;

    public int width = DefaultScreenWidth;
    public int height = DefaultScreenHeight;
    public bool fullscreen = true;

    void Awake()
    {
        Screen.SetResolution(width, height, fullscreen);
    }
}
