using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{

    public class TutorialManager : MonoBehaviour
    {
        // チュートリアルの種類
        private enum TutorialType
        {
            Move,
            Rotation,
            Click
        }

        // アニメーション名と対応づけ
        private readonly Dictionary<TutorialType, string> animationNames = new Dictionary<TutorialType, string>
    {
        { TutorialType.Move, "Move" },
        { TutorialType.Rotation, "Rotation" },
        { TutorialType.Click, "Click" }
    };

        [SerializeField] private Animator animator;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector2 rotationPos;
        [SerializeField] private Vector2 clickPos;

        // クリア済みを管理
        private HashSet<TutorialType> cleared = new HashSet<TutorialType>();

        private void Awake()
        {
            if (animator != null)
            {
                rectTransform = animator.GetComponent<RectTransform>();
            }
        }

        private void Start()
        {
            if (GameLoop.Instance != null)
            {
                GameLoop.Instance.StartAction += () => Clear(TutorialType.Click);
            }
            UpdateTutorial();
        }

        // 外部から呼ぶクリア処理
        public void ClearMove() => Clear(TutorialType.Move);
        public void ClearRotation() => Clear(TutorialType.Rotation);
        public void ClearClick() => Clear(TutorialType.Click);

        private void Clear(TutorialType type)
        {
            if (cleared.Contains(type)) return;

            cleared.Add(type);
            Debug.Log($"Tutorial Cleared: {type}");
            UpdateTutorial();
        }

        private void UpdateTutorial()
        {
            // 未クリアのものを探す
            foreach (TutorialType type in System.Enum.GetValues(typeof(TutorialType)))
            {
                if (!cleared.Contains(type))
                {
                    PlayTutorial(type);
                    return;
                }
            }

            // 全部クリア済み
            if (animator != null)
            {
                animator.gameObject.SetActive(false);
            }
        }

        private void PlayTutorial(TutorialType type)
        {
            if (animator == null) return;

            animator.SetTrigger(animationNames[type]);
            Debug.Log($"Play Tutorial: {type}");

            if (type == TutorialType.Rotation)
            {
                StartCoroutine(SetPositionDelayed(rotationPos));
            }
            else if (type == TutorialType.Click)
            {
                StartCoroutine(SetPositionDelayed(clickPos));
            }
        }

        private IEnumerator SetPositionDelayed(Vector2 pos)
        {
            yield return new WaitForSeconds(1f);
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = pos;
                Debug.Log($"Position Set: {pos}");
            }
        }
    }

}
