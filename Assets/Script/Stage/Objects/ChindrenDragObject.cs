using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class ChindrenDragObject : DragObject
    {
        ChildData[] children;
        protected override void Awake()
        {
            children = new ChildData[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = new ChildData(transform.GetChild(i).gameObject);
                if(transform.GetChild(i).GetComponent<ChildDragObject>()==null) transform.GetChild(i).gameObject.AddComponent<ChildDragObject>();
            }
        }
        protected override void StartDragDerivation(Vector3 inputScreenPos)
        {
            foreach (var c in children) c.backSprite.transform.localScale *= 1.05f;
            Debug.Log("*1.05");
        }

        protected override void DragDerivation(Vector3 inputScreenPos)
        {
            foreach (var c in children) c.outSprite.gameObject.SetActive(!IsPlaced());
        }

        protected override void EndDragDerivation(Vector3 inputScreenPos)
        {
            foreach (var c in children)
            {
                c.backSprite.transform.localScale /= 1.05f;
                c.outSprite.gameObject.SetActive(false);
            }
            Debug.Log("/1.05");
        }

        public override bool IsPlaced()
        {
            if (!InRange() || IsOverLapping())
            {
                if (IsFramePositionAvailable()) return true;
                else return false;
            }
            return true;
        }
        protected override bool CanDrag()
        {
            if (GameLoop.StageState != StageState.Setting) return false;
            return true;
        }

        protected override bool IsOverLapping()
        {
            // 軽量な ContactFilter2D の生成（都度 struct をローカルで作るのが正解）
            ContactFilter2D filter = new ContactFilter2D
            {
                useTriggers = false,
                useLayerMask = true,
                layerMask = Physics2D.DefaultRaycastLayers
            };

            // 再利用可能な結果リスト
            List<Collider2D> results = new List<Collider2D>(8);

            // 自身の Collider を除外対象として登録
            HashSet<Collider2D> selfColliders = new HashSet<Collider2D>();
            foreach (var c in children)
            {
                if (c.colli != null)
                    selfColliders.Add(c.colli);
            }

            // 各 Collider をチェック（他者と重なっていたら即 true）
            foreach (var c in children)
            {
                if (c.colli == null) continue;

                results.Clear();
                int hitCount = c.colli.OverlapCollider(filter, results);

                for (int i = 0; i < hitCount; i++)
                {
                    if (!selfColliders.Contains(results[i]))
                        return true;
                }
            }

            return false;
        }


        public void OnMouseEnterExit(bool enter)
        {
            foreach(var c in children)c.backSprite.gameObject.SetActive(enter);
        }


        protected override void OrderInLayerChange()
        {
            foreach(var c in children)
            {
                if (IsDragging)
                {
                    c.sprite.sortingOrder = (int)OrderInLayer.DragNow;
                    c.backSprite.LayerChange((int)OrderInLayer.DragBackSprite);
                }
                else
                {
                    c.sprite.sortingOrder = (int)OrderInLayer.NoDrag;
                    c.backSprite.LayerChange((int)OrderInLayer.StillnessBackSprite);
                }
            }
        }

        protected override bool InRange()
        {
            foreach(var c in children)
            {
                if (!ObjectRange.Instance.InRange(c.sprite))
                {
                    return false;
                }
            }
            return true;
        }

        public override void FitSpriteInSquare(SpriteRenderer frame)
        {

            bool hasValidSprite = false;
            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;

            // すべてのスプライトのAABBを求める
            foreach (var data in children)
            {
                if (data.sprite == null) continue;

                Bounds bounds = data.sprite.bounds; // ワールド座標のバウンディングボックス

                min = Vector3.Min(min, bounds.min);
                max = Vector3.Max(max, bounds.max);
                hasValidSprite = true;
            }

            if (!hasValidSprite)
            {
                Debug.LogWarning("有効なスプライトがchildDataに存在しません");
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
            Vector3 offset =  frame.transform.position - GetCenterOfChildSprites();
            Debug.Log("offset+"+offset);
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
