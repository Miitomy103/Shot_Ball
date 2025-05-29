using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngineInternal;

namespace ShotBall.InGame
{
    public class Ball : MonoBehaviour,IAntiGravity,IKeyNumber
    {
        private Rigidbody2D rigid;
        public new CircleCollider2D collider { get; private set; }

        [SerializeField] ParticleSystem dieEffect;

        GameObject childObject;

        const float localGravity = 9.81f;

        bool antiGravity;
        public bool AntiGravity => antiGravity;

        [SerializeField] AudioSource antiGravitySound;
        [SerializeField] AudioSource gravitySound;

        [SerializeField] AudioSource dieSound;

        public int KeyNumber { get; set; }

        private SpriteRenderer thisSprite;

        bool timeStop;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            collider = GetComponent<CircleCollider2D>();
            thisSprite = GetComponent<SpriteRenderer>();
            childObject = transform.GetChild(0).gameObject;
        }
        private void Start()
        {
            if(GravitySwitch.Instance!=null)
            {
                GravitySwitch.Instance.Action += GravitySwitchChange;
            }
        }
        private void FixedUpdate()
        {
            SetLocalGravity(); //重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。

            if (!ObjectRange.Instance.InRange(thisSprite))
            {
                StartCoroutine(RangeCoroutine());
            }
        }
        void GravitySwitchChange()
        {
            ChangeGravity();
        }
        IEnumerator RangeCoroutine()
        {
            Debug.Log("CoroutineSTart");
            yield return new WaitForSeconds(1.5f);
            if(!ObjectRange.Instance.InRange(thisSprite))
            {
                Debug.Log("CoroutineStart");
                Die();
            }
        }
        public void Die()
        {
            if (GravitySwitch.Instance != null)
            {
                GravitySwitch.Instance.Action -= GravitySwitchChange;
            }
            dieSound.PlayOneShot(dieSound.clip);
            gameObject.SetActive(false);
            GameObject eff = Instantiate(dieEffect.gameObject, transform.position, dieEffect.transform.rotation);
            Destroy(eff, 3f);
            GameLoop.Instance.GameReset();
        }
        private void SetLocalGravity()
        {
            rigid.AddForce(GetGravity());
        }
        Vector2 GetGravity()
        {
            if (timeStop) return Vector2.zero;
            return (AntiGravity ? Vector2.up : Vector2.down) * localGravity;
        }

        public void PlayStart(int key)
        {
            KeyNumber= key;
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
        public void Clear()
        {
            gameObject.SetActive(false);
            GameLoop.Instance.ClearAreaCheck(this);
        }

        public void ChangeGravity()
        {
            if (rigid == null) return;
            antiGravity = !AntiGravity;
            rigid.velocity = rigid.velocity / 2;
            childObject.SetActive(antiGravity);
            Debug.Log("ChangeGravity");

            if (GameLoop.StageState == StageState.Playing)
            {
                if (antiGravity) antiGravitySound.Play();
                else gravitySound.Play();
            }
        }

        public void ColorChange(Color color)
        {
            thisSprite.color = color;
        }
        public void TimeStop(float time)
        {
            StartCoroutine(PausePhysics(time));
        }
        private IEnumerator PausePhysics(float duration)
        {
            timeStop = true;
            // 停止前の速度を保存
            Vector2 savedVelocity = rigid.velocity;
            float savedAngularVelocity = rigid.angularVelocity;
            rigid.velocity = Vector2.zero;

            // 物理挙動を停止
            rigid.isKinematic = true;

            // 一定時間待つ
            yield return new WaitForSeconds(duration);

            // 再び物理挙動を有効にして、元の速度を復元
            rigid.isKinematic = false;
            rigid.velocity = savedVelocity;
            rigid.angularVelocity = savedAngularVelocity;
            timeStop = false;
        }
    }
}
