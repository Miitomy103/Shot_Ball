using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    public int width = 1920;
    public int height = 1080;
    public bool fullscreen = true;

    void Start()
    {
        Screen.SetResolution(width, height, fullscreen);
    }
}
