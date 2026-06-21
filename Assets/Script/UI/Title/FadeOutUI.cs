using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// UI画像をフェードイン・フェードアウトさせるクラス。
/// </summary>
public class FadeOutUI : MonoBehaviour
{
    [SerializeField]Image Image;
    [SerializeField] float fadeSpeed = 1f;

    [SerializeField] UnityEvent onFadeOutComplete;
    // Start is called before the first frame update
    void Start()
    {
        Color color = Image.color;
        color.a = 1f;
        Image.color = color;
        StartCoroutine(FadeOut());
    }

    /// <summary>
    /// 不透明から透明へフェードアウトさせ、完了後にオブジェクトを非表示にする。
    /// </summary>
    IEnumerator FadeOut()
    {
        Color color = Image.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            Image.color = color;
            yield return null;
        }
        color.a = 0f;
        Image.color = color;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// オブジェクトを表示状態にし、フェードインを開始する。
    /// </summary>
    public void FadeInStart()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
    /// <summary>
    /// 透明から不透明へフェードインさせ、完了後にonFadeOutCompleteを発火する。
    /// </summary>
    IEnumerator FadeIn()
    {
        Color color = Image.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime * fadeSpeed;
            Image.color = color;
            yield return null;
        }
        color.a = 1f;
        Image.color = color;
        onFadeOutComplete.Invoke();
    }
}
