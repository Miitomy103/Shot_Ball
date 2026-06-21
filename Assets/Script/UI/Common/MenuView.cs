using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// メニュー画面の表示/非表示を切り替え、GameLoopの状態と同期させるクラス。
    /// </summary>
    public class MenuView : MonoBehaviour
    {
        [SerializeField] GameObject menu;
        /// <summary>
        /// メニューを開く。
        /// </summary>
        public void OnMenu()
        {
            menu.SetActive(true);
            GameLoop.Instance.OnMenu();
        }
        /// <summary>
        /// メニューを閉じる。
        /// </summary>
        public void OffMenu()
        {
            menu.SetActive(false);
            GameLoop.Instance.OffMenu();
        }
    }
}
