using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class EditorDragObject : ObjectBase
    {
        Collider2D[] collis;
        private void Awake()
        {
            collis = GetComponentsInChildren<Collider2D>();
        }
        void Start()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.sortingOrder = (int)OrderInLayer.NoDrag;
                return;
            }
        }
        [SerializeField] LayerMask ballLayer = 6;

        protected override bool IsOverLapping()
        {
            // 除外したいレイヤーを除いたマスクを生成
            LayerMask excludeMask = Physics2D.DefaultRaycastLayers & ~ballLayer;

            ContactFilter2D filter = new ContactFilter2D
            {
                useTriggers = false,
                useLayerMask = true,
                layerMask = excludeMask
            };

            List<Collider2D> results = new List<Collider2D>(8);
            HashSet<Collider2D> selfColliders = new HashSet<Collider2D>();

            foreach (var c in collis)
            {
                if (c != null)
                    selfColliders.Add(c);
            }

            foreach (var c in collis)
            {
                if (c == null) continue;

                results.Clear();
                int hitCount = c.OverlapCollider(filter, results);

                for (int i = 0; i < hitCount; i++)
                {
                    if (!selfColliders.Contains(results[i]))
                        return true;
                }
            }

            return false;
        }

    }
}
