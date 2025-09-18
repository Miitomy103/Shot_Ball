using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ChildFitInSprite : MonoBehaviour,IFitSpriteInSquare
    {
        SpriteRenderer[] children;
        private void Awake()
        {
            SpriteRenderer[] c = GetComponentsInChildren<SpriteRenderer>();
            children = c.Where(t => t != null && t.gameObject != this.gameObject).ToArray();
        }
        public void FitSprite(SpriteRenderer frame)
        {
            // frameがnullなら自分にアタッチされているSpriteRendererを使う
            if (frame == null)
            {
                frame = GetComponent<SpriteRenderer>();
                if (frame == null)
                {
                    Debug.LogError("FitSprite: frameがnullです。SpriteRendererを取得できません。");
                    return;
                }
            }

            if (children == null || children.Length == 0)
            {
                Debug.LogWarning("FitSprite: childrenがnullまたは空です");
                return;
            }

            bool hasValidSprite = false;
            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;

            // すべてのスプライトのAABBを求める
            foreach (var data in children)
            {
                if (data == null || data.sprite == null) continue;

                Bounds bounds = data.sprite.bounds; // ワールド座標のバウンディングボックス

                min = Vector3.Min(min, bounds.min);
                max = Vector3.Max(max, bounds.max);
                hasValidSprite = true;
            }

            if (!hasValidSprite)
            {
                Debug.LogWarning("FitSprite: 有効なスプライトがchildDataに存在しません");
                return;
            }

            // スプライト全体のサイズと中心を計算
            Vector3 totalSize = max - min;
            Vector3 currentCenter = (max + min) / 2f;

            // スケール倍率（frameに収める）
            float maxDimension = Mathf.Max(totalSize.x, totalSize.y);
            if (maxDimension == 0)
            {
                transform.localScale = Vector3.zero;
                return;
            }

            float targetScale = frame.bounds.size.x * 0.85f / maxDimension;
            transform.localScale = Vector3.one * targetScale;

            // スプライト全体の中心をframeの中心に合わせる
            Vector3 offset = frame.transform.position - GetCenterOfChildSprites();
            transform.position += offset;
        }

        public Vector3 GetCenterOfChildSprites()
        {
            Bounds bounds = children[0].sprite.bounds;

            for (int i = 1; i < children.Length; i++)
            {
                bounds.Encapsulate(children[i].sprite.bounds);
            }

            Debug.Log("bounds.center;" + bounds.center);
            return bounds.center;
        }
    }
}
