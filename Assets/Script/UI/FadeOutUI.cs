using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutUI : MonoBehaviour
{
    [SerializeField]Image Image;
    [SerializeField] float fadeSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
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
}
