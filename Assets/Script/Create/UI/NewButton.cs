using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// テクスチャ画像を表示する「新規作成」ボタンのクラス。
    /// </summary>
    public class NewButton : MonoBehaviour,INullDisplay
    {
        [SerializeField] Texture2D texture;
        [SerializeField] Vector2 sizeDelta = new Vector2(150, 150);
        [SerializeField] Color color = Color.white;

        RawImage rawImage;
        private void Awake()
        {
            rawImage = GetComponentInChildren<RawImage>();
        }
        /// <summary>
        /// 設定されたテクスチャ・サイズ・色をRawImageに反映する。
        /// </summary>
        public void Display()
        {
            rawImage.texture = texture;
            rawImage.rectTransform.sizeDelta = sizeDelta;
            rawImage.color = color;
        }
    }
    /// <summary>
    /// 表示処理を持つことを表すインターフェース。
    /// </summary>
    public interface INullDisplay
    {
        /// <summary>
        /// 表示する。
        /// </summary>
        void Display();
    }
}
