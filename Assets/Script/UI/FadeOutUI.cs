using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public void FadeInStart()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
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
