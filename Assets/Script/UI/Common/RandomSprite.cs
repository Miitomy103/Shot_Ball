using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
/// <summary>
/// 開始時に配列からランダムに1枚選んでImageに表示するクラス。
/// </summary>
public class RandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        Image spriteRenderer = GetComponent<Image>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
