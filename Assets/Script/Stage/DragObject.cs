using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class DragObject : MonoBehaviour
    {

        protected SpriteRenderer thisSprite { get; private set; }

        private void OnEnable()
        {
            thisSprite = GetComponent<SpriteRenderer>();
        }

        protected virtual void Awake()
        {
            if (thisSprite == null) thisSprite = GetComponent<SpriteRenderer>();
        }
    }

}
