using UnityEngine;

namespace ShotBall.InGame
{
    public class MenuView : MonoBehaviour
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
