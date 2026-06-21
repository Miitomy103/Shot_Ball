using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 一時的なログメッセージをUI上に表示し、一定時間後に自動で隠すクラス。
/// </summary>
public class LogDisplay : MonoBehaviour
{
    private static LogDisplay instance;
    public static LogDisplay Instance => instance;

    [SerializeField] private Vector2 padding = new Vector2(10f, 10f); // 余白

    [SerializeField] TextMeshProUGUI logText;
    [SerializeField] RectTransform image;

    [SerializeField] float defaultDisplayTime = 1f;
    private float startTime;

    private IEnumerator hideCoroutine;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startTime=defaultDisplayTime;
    }

    /// <summary>
    /// 指定したテキストをログとして表示し、表示時間が過ぎたら自動的に隠す。
    /// </summary>
    public void SetLog(string text)
    {
        logText.text = text;
        StartCoroutine(AdjustSizeNextFrame());



        // Textの推奨サイズを取得
        float width = logText.preferredWidth;
        float height = logText.preferredHeight;

        // Imageのサイズを調整
        image.sizeDelta = new Vector2(width + padding.x, height + padding.y);


        image.gameObject.SetActive(true);

        StartCoroutine(HideLog());
        hideCoroutine = HideLog();
    }
    /// <summary>
    /// ログの表示時間を設定する。
    /// </summary>
    public void SetTime(float time)
    {
        defaultDisplayTime = time;
    }

    private IEnumerator AdjustSizeNextFrame()
    {
        yield return null; // 1フレーム待つ
        float width = logText.preferredWidth;
        float height = logText.preferredHeight;
        image.sizeDelta = new Vector2(width + padding.x, height + padding.y);
    }


    /// <summary>
    /// 表示時間が過ぎたあとログ表示を隠すコルーチン。
    /// </summary>
    public IEnumerator HideLog()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        yield return new WaitForSeconds(defaultDisplayTime);
        image.gameObject.SetActive(false);
        SetTime(defaultDisplayTime);
    }
}
