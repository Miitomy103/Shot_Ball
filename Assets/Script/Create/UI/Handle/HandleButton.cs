using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// 選択したオブジェクトを拡大縮小するためのUI
    /// これを4つおく
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class HandleButton : MonoBehaviour
    {
        [SerializeField] private RectTransform line;
        [SerializeField] private Vector2 offset;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            HandleFollower.Instance.OnResize += SetPosition;
        }

        public void SetPosition()
        {
            float angle = line.localEulerAngles.z * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            Vector2 rotatedOffset = new Vector2(
                offset.y * cos - offset.x * sin,
                offset.y * sin + offset.x * cos
            );

            rectTransform.anchoredPosition = line.anchoredPosition + rotatedOffset;
        }


    }

}
