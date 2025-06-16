using ShotBall.InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class CreateObject : MonoBehaviour
    {
        SpriteRenderer ThisSprite;

        private void Awake()
        {
            ThisSprite = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            
        }
        private void OnMouseDown()
        {
            Debug.Log("CreateObject OnMouseDown");
            HandleFollower.Instance.SpriteSet(ThisSprite, true);
        }
    }
}
