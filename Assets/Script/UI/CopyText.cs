using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class CopyPasteText : MonoBehaviour
    {
        [SerializeField]Text copyText;
        public void Copy()
        {
            if(string.IsNullOrEmpty(copyText.text))
            {
                return;
            }
            //クリップボードにコピーする文字列
            string coptyText = copyText.text;

            //クリップボードへ文字を設定(コピー)
            GUIUtility.systemCopyBuffer = coptyText;

            LogDisplay.Instance.SetLog("クリップボードにコピーしました");
        }
        public void Paste()
        {
            //クリップボードから文字を取得(ペースト)
            string pasteText = GUIUtility.systemCopyBuffer;
            if (string.IsNullOrEmpty(pasteText))
            {
                return;
            }
            copyText.text = pasteText;
            LogDisplay.Instance.SetLog("クリップボードからペーストしました");
        }
    }
}
