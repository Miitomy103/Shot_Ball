using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// 子の複数のSpriteRendererをまとめてSliced表示で任意サイズに引き伸ばし、Collider2Dのサイズも追従させる基底クラス。
    /// </summary>
    public abstract class SpriteImages : MonoBehaviour
    {
        private SpriteRenderer[] sprites;
        protected Collider2D colli;

        [SerializeField] protected Vector2 size = new Vector2(1, 1);

        void Awake()
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
            colli = GetComponent<Collider2D>();
            SetupSpriteRenderer();
        }

#if UNITY_EDITOR
        // Editorで変更を反映させるため
        void OnValidate()
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
            if (colli == null) colli = GetComponent<Collider2D>();
            SetupSpriteRenderer();
        }
#endif

        private void SetupSpriteRenderer()
        {
            foreach (var sr in sprites)
            {
                if (sr.sprite == null) return;

                sr.drawMode = SpriteDrawMode.Sliced;
                // スプライトにBorderが設定されてないとSlicedが効かないので注意
                sr.size = size;
            }

            ColliderSizeChange();
        }

        /// <summary>
        /// 派生クラスでColliderの種類に応じたサイズ反映処理を行う。
        /// </summary>
        protected abstract void ColliderSizeChange();

    }
}
