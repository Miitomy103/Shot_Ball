using ShotBall.InGame;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Bouncy : CollisionBase
    {
        [SerializeField] float bounceForce = 10f; // 跳ねる強さ

        [SerializeField] AnimationCurve curve;

        [SerializeField] AudioSource bouncySound;

        [SerializeField] TextMeshPro text;

        protected override void Start()
        {
            base.Start();

            float b= bounceForce / 10f;
            // 一時的にPhysicsMaterial2Dを作成
            PhysicsMaterial2D tempMat = new PhysicsMaterial2D();
            tempMat.bounciness = 1.25f*b;    // 跳ね返り
            tempMat.friction = 0f;       // 摩擦

            // このオブジェクトのCollider2Dに設定
            Collider2D col = GetComponent<Collider2D>();
            col.sharedMaterial = tempMat;
        }
        protected override void BallEnter(Collision2D collision)
        {
            StartCoroutine(BoundAnimation());
            bouncySound.Play();
        }
        private IEnumerator BoundAnimation()
        {
            float duration = 0.25f;
            float timer = 0;

            Vector3 startScale = transform.localScale;
            Vector3 endScale = transform.localScale -= new Vector3(-transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, 0);

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;

                // カーブから補間値を取得
                float curveValue = curve.Evaluate(t);

                // スケールを補間
                transform.localScale = Vector3.Lerp(startScale, endScale, curveValue);

                yield return new WaitForEndOfFrame();
            }
            timer = 0;
            while (startScale.y > transform.localScale.y)
            {
                timer += Time.deltaTime;
                float t = timer / duration;

                // カーブから補間値を取得
                float curveValue = curve.Evaluate(t);

                // スケールを補間
                transform.localScale = Vector3.Lerp(endScale, startScale, curveValue);

                yield return new WaitForEndOfFrame();
            }
            //transform.localScale = startScale;
        }

        protected override string StringData()
        {
            return $"{bounceForce}";
        }
        public override void LoadData(string data)
        {
            Debug.Log($"Bouncy LoadData: {data}");
            base.LoadData(data);
            string[] splitData = data.Split(',');
            bounceForce = float.Parse(splitData[0]);
        }

        public override void LoadData(StageBlockData data)
        {
            if (data.floatParametors.TryGetValue("BounceForce", out float bounceValue))
            {
                bounceForce = bounceValue;
            }
            else
            {
                Debug.LogWarning("BounceForce parameter not found in Bouncy data.");
            }
        }
    }
}
