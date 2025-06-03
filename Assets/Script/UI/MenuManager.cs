using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] GameObject menu;
        public void OnMenu()
        {
            menu.SetActive(true);
            GameLoop.Instance.OnMenu();
        }
        public void OffMenu()
        {
            menu.SetActive(false);
            GameLoop.Instance.OffMenu();
        }
    }
}
