using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public struct ChildData
    {
        public GameObject gameObject;
        public SpriteRenderer sprite;
        public Collider2D colli;
        public GimmickBase gimmick;
        public SubSprite backSprite;
        public SubSprite outSprite;

        public ChildData(GameObject game)
        {
            gameObject = game;
            sprite = game.GetComponent<SpriteRenderer>();
            colli = game.GetComponent<Collider2D>();
            gimmick = game.GetComponent<GimmickBase>();
            foreach(Transform c in gameObject.transform)
            {
                Object.Destroy(c.gameObject);
            }
            backSprite = new SubSprite(game.transform, sprite, new Color(0.1f, 0.1f, 0.1f, 0.75f), 1.1f);
            outSprite = new SubSprite(game.transform, sprite, new Color(1f, 0, 0, 0.85f));
            outSprite.LayerChange((int)(OrderInLayer.OutSprite));
        }
        public ChildData(GameObject game,SpriteRenderer s,Collider2D c,GimmickBase  gi)
        {
            gameObject = game;
            sprite = s;
            colli = c;
            gimmick = gi;
            backSprite = new SubSprite(game.transform, sprite, new Color(0.1f, 0.1f, 0.1f, 0.75f), 1.1f);
            outSprite = new SubSprite(game.transform, sprite, new Color(1f, 0, 0, 0.85f));
        }
    }
}
