using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class DragObject : MonoBehaviour
    {

        protected SpriteRenderer thisSprite { get; private set; }

        [SerializeField] Vector2 spriteSize = new Vector2(1, 1);

        private void OnEnable()
        {
            thisSprite = GetComponent<SpriteRenderer>();

            //thisSprite.drawMode = SpriteDrawMode.Sliced;
            //thisSprite.size = spriteSize;
        }

        protected virtual void Awake()
        {
 
        }
    }

}
