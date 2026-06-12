using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace ShotBall.InGame.Animation
{
    /// <summary>
    /// コインを取得したときのアニメーション
    /// </summary>
    public class CoinAnimation : MonoBehaviour
    {
        private const float MoveHeight = 0.75f;
        private const float RotateDurationSeconds = 0.5f;

        SpriteRenderer thisSprite;
        [SerializeField] Sprite[] sprites;

        Vector3 movePos = new Vector3(0f, MoveHeight);

        IEnumerator coroutine;

        private void Awake()
        {
            thisSprite = GetComponent<SpriteRenderer>();
        }

        public void Rotate(int value,Action action=null)
        {
            coroutine = RotateCoroutine(value,action);
            StartCoroutine(coroutine);
        }
        public void RotateStop()
        {
            if(coroutine!=null)
            {
                StopCoroutine(coroutine);
                thisSprite.sprite = sprites[0];
            }
        }
        IEnumerator RotateCoroutine(int value, Action action)
        {
            float duration = RotateDurationSeconds; // 想定再生時間
            float elapsed = 0f;

            Vector3 start = transform.position;
            Vector3 end = start + movePos;

            int spriteIndex = 0;
            int totalFrames = sprites.Length * value;

            while (elapsed < duration)
            {
                // スプライト更新
                spriteIndex = Mathf.FloorToInt((elapsed / duration) * totalFrames) % sprites.Length;
                thisSprite.sprite = sprites[spriteIndex];

                // 移動
                float t = elapsed / duration;
                transform.position = Vector3.Lerp(start, end, t);

                elapsed += Time.deltaTime;
                yield return null;
            }

            // 最終位置補正
            thisSprite.sprite = sprites[0];
            transform.position = end;
            coroutine = null;
            action?.Invoke();
        }

    }
}
