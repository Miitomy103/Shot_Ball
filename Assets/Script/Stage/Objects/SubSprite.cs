using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class SubSprite : ScriptableObject
    {
        Transform parentTrans { get; }
        public Transform transform { get; set; }
        SpriteRenderer thisSprite;

        public GameObject gameObject { get; private set; }
        public SubSprite(Transform parent, SpriteRenderer parentSprite, Color color, float scaleMultiple = 1)
        {
            parentTrans = parent;

            gameObject = new GameObject("SubSprite");
            transform = gameObject.transform;
            thisSprite = gameObject.AddComponent<SpriteRenderer>();
            thisSprite.sprite = parentSprite.sprite;

            Quaternion quaternion = transform.localRotation;
            // 親子関係を設定
            transform.parent = parentTrans;

            // Transform関連の設定（位置・回転・スケール）
            transform.localPosition = Vector3.zero;

            // 親の回転を打ち消す（回転だけ独立させる）
            transform.localRotation = Quaternion.Inverse(parentTrans.localRotation);

            // スケールは親の影響＋拡大したい分だけ乗算（通常はVector3.one * 1.1fでもOK）
            transform.localScale = Vector3.one * scaleMultiple;

            transform.localRotation = quaternion;

            // SpriteRendererの設定
            thisSprite.drawMode = SpriteDrawMode.Sliced;
            thisSprite.color = color;
            //OrderInLayerChange(placeOrther);
            thisSprite.size = parentSprite.size;
        }

        public void LayerChange(int orderInLayer)
        {
            thisSprite.sortingOrder = orderInLayer;
        }
    }
}
