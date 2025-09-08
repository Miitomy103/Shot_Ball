using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class SpriteImage : MonoBehaviour
    {
        private SpriteRenderer sr;
        protected Collider2D colli;

        [SerializeField] protected Vector2 size = new Vector2(1, 1);

        void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            colli = GetComponent<Collider2D>();
            SetupSpriteRenderer();
        }

#if UNITY_EDITOR
        // Editorで変更を反映させるため
        void OnValidate()
        {
            if (sr == null) sr = GetComponent<SpriteRenderer>();
            if (colli == null) colli = GetComponent<Collider2D>();
            SetupSpriteRenderer();
        }
#endif
        public void SizeChange(Vector2 newSize)
        {
            size = newSize;
            SetupSpriteRenderer();
        }
        private void SetupSpriteRenderer()
        {
            if (sr.sprite == null) return;

            sr.drawMode = SpriteDrawMode.Sliced;

            // スプライトにBorderが設定されてないとSlicedが効かないので注意
            sr.size =size;

            ColliderSizeChange();
        }

        protected abstract void ColliderSizeChange();

    }
}
