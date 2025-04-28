using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Ball : MonoBehaviour,IAntiGravity
    {
        new Rigidbody2D rigidbody;

        const float localGravity = 9.81f;

        public bool AntiGravity { get ; set ; }


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
        }
        private void Start()
        {

        }
        private void FixedUpdate()
        {
            SetLocalGravity(); //重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。
        }

        private void SetLocalGravity()
        {
            rigidbody.AddForce(GetGravity());
        }
        Vector2 GetGravity()
        {
            return (AntiGravity ? Vector2.up : Vector2.down) * localGravity;
        }

        public void PlayStart()
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public void ChangeGravity()
        {
            AntiGravity = !AntiGravity;
            rigidbody.velocity = rigidbody.velocity / 2;
            Debug.Log("ChangeGravity");
        }
    }
}
