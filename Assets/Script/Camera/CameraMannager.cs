using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CameraManager : MonoBehaviour
    {
        Camera cam => Camera.main;
        private void Awake()
        {
#if UNITY_STANDALONE
            float aspect = (float)Screen.width / Screen.height;
            if (aspect > 1.6f)
            {
                cam.orthographicSize = 5f;
            }
            else if(aspect<=1.6f)
            {
                cam.orthographicSize = 5.7f;
            }
#endif
        }

    }
}
