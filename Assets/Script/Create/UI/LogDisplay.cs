using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    public IEnumerator HideLog()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        yield return new WaitForSeconds(defaultDisplayTime);
        image.gameObject.SetActive(false);
        SetTime(defaultDisplayTime);
    }
}
