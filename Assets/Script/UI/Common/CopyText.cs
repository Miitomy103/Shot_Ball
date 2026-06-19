using UnityEngine;
using UnityEngine.UI;

public class CopyPasteText : MonoBehaviour
{
    [SerializeField] Text copyText;
    public void Copy()
    {
        if (string.IsNullOrEmpty(copyText.text))
        {
            return;
        }
        //クリップボードにコピーする文字列
        string value = copyText.text;

        //クリップボードへ文字を設定(コピー)
        GUIUtility.systemCopyBuffer = value;

        if(LogDisplay.Instance!=null) LogDisplay.Instance.SetLog("クリップボードにコピーしました");
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
        if (LogDisplay.Instance != null) LogDisplay.Instance.SetLog("クリップボードからペーストしました");
    }
}
