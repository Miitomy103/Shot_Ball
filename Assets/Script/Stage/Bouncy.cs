using ShotBall.InGame;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bouncy : CollisionBase
{
    [SerializeField] float bounceForce = 10f; // 跳ねる強さ

    [SerializeField] AnimationCurve curve;

    [SerializeField] AudioSource bouncySound;

    public override string Name => "Bouncy";

    [SerializeField] TextMeshPro text;

    protected override void Start()
    {
        base.Start();
        float value = bounceForce;
        float roundedValue = Mathf.Round(value * 10f) / 10f;
        text.text = roundedValue.ToString();
        text.gameObject.SetActive(false);
    }
    //private void OnMouseEnter()
    //{
    //    text.gameObject.SetActive(true);
    //}
    //private void OnMouseExit()
    //{
    //    text.gameObject.SetActive(false);
    //}
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
        Vector3 endScale = transform.localScale -= new Vector3(-transform.localScale.x*0.5f, transform.localScale.y*0.5f, 0);

        while(timer<=duration)
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
        while(startScale.y>transform.localScale.y)
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
}
