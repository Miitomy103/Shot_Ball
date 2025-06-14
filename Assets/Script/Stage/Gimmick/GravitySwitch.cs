using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace ShotBall.InGame
{
    public class GravitySwitch : AreaBase,IAntiGravity,IOnOff
    {
        static GravitySwitch instance;
        public static GravitySwitch Instance => instance;
        public static Color color { get; private set; }

        [SerializeField] bool startAntiGravity;
        public bool StartAntiGravity => startAntiGravity;

        GameObject child=>transform.GetChild(0).gameObject; // 最初の子

        Camera cam => Camera.main;

        static bool antiGravity;

        public Action Action { get; set; }

        public bool AntiGravity => antiGravity;

        private bool isOn;
        public bool IsOn => isOn;

        public Transform Transform => transform;

        public override string Name => "GravitySwitch";

        protected override void Awake()
        {
            base.Awake();
            instance = this;
        }
        protected override void Start()
        {
            base.Start();
            color = cam.backgroundColor;
            if(StartAntiGravity)
            {
                ChangeGravity();
                child.transform.localScale = ChildCircleSize();
            }
        }
        protected override void StageReset()
        {
            base.StageReset();
            ChangeGravity(StartAntiGravity);
            child.transform.localScale = Vector3.zero;
        }
        protected override void BallEnter(Ball other)
        {
            ChangeGravity();
            StartCoroutine(MakeItBigger(child.transform));
        }
        IEnumerator MakeItBigger(Transform transform)
        {
            if (GameLoop.StageState != StageState.Playing) yield break;
            float duration = 0.75f;
            float timer = 0;

            Vector3 startScale = transform.localScale;
            Vector3 endScale = (AntiGravity) ? ChildCircleSize(): Vector3.zero;

            while (timer <= duration)
            {
                timer += Time.deltaTime;

                // カーブから補間値を取得
                float t = Mathf.Clamp01(timer / duration); // tを0～1に制限

                // スケールを補間
                transform.localScale = Vector3.Lerp(startScale, endScale, t);

                yield return new WaitForEndOfFrame();
            }

        }

        public void ChangeGravity()
        {
            antiGravity = !AntiGravity;
            Action?.Invoke();
        }
        public void ChangeGravity(bool a)
        {
            antiGravity = a;
            Action?.Invoke();
        }

        Vector3 ChildCircleSize()
        {
            float height = cam.orthographicSize * 2f;
            float width = height * cam.aspect;
            return new Vector3(width, width) * 2;
        }

        public void ItOn()
        {
            isOn = true;
            ChangeGravity(true);
            StartCoroutine(MakeItBigger(child.transform));
        }

        public void ItOff()
        {
            isOn=false;
            ChangeGravity(false);
            StartCoroutine(MakeItBigger(child.transform));
        }

        protected override string StringData()
        {
            return startAntiGravity.ToString();
        }
        public override void LoadData(string data)
        {
            base.LoadData(data);
            string[] datas = data.Split(',');
            startAntiGravity = bool.Parse(datas[0]);
        }
    }
}
