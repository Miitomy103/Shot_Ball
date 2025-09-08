using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.InGame
{
    public class ArrowToggle : MonoBehaviour
    {
        public Vector2 Direction { get; private set; } = Vector2.right;

        [SerializeField] Button button;
        [SerializeField] RectTransform targetUI;

        public Action<Vector2> Checked { get; set; }

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            SetValue(RotateAfter(Direction));
        }
        public void SetValue(Vector2 value)
        {
            Direction = value.normalized; // ベクトルを正規化して方向を設定
            SetDirection(value);
            Checked?.Invoke(value);
        }
        public void SetDirection(Vector2 direction)
        {
            if (direction == Vector2.zero) return; // 無効な方向は無視

            // 1. direction ベクトルから角度を計算
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 2. UI をその角度に回転
            targetUI.rotation = Quaternion.Euler(0, 0, angle);

            Debug.Log("UI rotated to direction: " + direction + " (angle: " + angle + ")");
        }
        Vector2 RotateAfter(Vector2 direction)
        {
            // 1. 現在の方向ベクトルの角度を取得
            float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 2. 45度回転（時計回り）
            float newAngle = currentAngle - 45f;

            // 3. direction を更新
            float rad = newAngle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }
    }
}
