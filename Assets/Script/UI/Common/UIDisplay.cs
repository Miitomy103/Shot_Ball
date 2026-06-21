using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall
{
    /// <summary>
    /// 1つの表示用オブジェクトを生成・差し替え表示するクラス。何も表示していない場合はnullObjectを表示する。
    /// </summary>
    public class UIDisplay : MonoBehaviour
    {
        [SerializeField] private float displaySizeMagnification = 1;
        [SerializeField] private RectTransform nullObject;

        GameObject displayObject;

        /// <summary>
        /// 表示中のオブジェクトを破棄し、空状態の表示(nullObject)に戻す。
        /// </summary>
        public void NullDisplay()
        {
            nullObject.gameObject.SetActive(true);
            if (displayObject != null)
            {
                Destroy(displayObject);
                displayObject = null;
            }
        }
        /// <summary>
        /// 指定したプレハブを複製して表示する。
        /// </summary>
        public void GenerateDisplay(GameObject obj)
        {
            RectTransform r=Instantiate(obj.gameObject, transform).GetComponent<RectTransform>();
            SetDisplay(r);
        }
        /// <summary>
        /// 表示中のオブジェクトを差し替え、サイズ倍率を適用して表示する。
        /// </summary>
        public void SetDisplay(RectTransform rectTransform)
        {
            if (displayObject != null)
            {
                Destroy(displayObject);
            }
            nullObject.gameObject.SetActive(false);
            displayObject = rectTransform.gameObject;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * displaySizeMagnification, rectTransform.sizeDelta.y * displaySizeMagnification);
        }
    }
}
