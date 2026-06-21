using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ShotBall.InGame
{
    /// <summary>
    /// 指定した待機時間の後にイベントを発火する汎用クラス。
    /// </summary>
    public class CoroutineEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent UnityEvent;
        [SerializeField] float waitTime = 1f;
        /// <summary>
        /// 待機後にイベントを発火するコルーチンを開始する。
        /// </summary>
        public void StartCoroutineEvent()
        {
            StartCoroutine(WaitAndInvoke());
        }
        private IEnumerator WaitAndInvoke()
        {
            yield return new WaitForSeconds(waitTime);
            UnityEvent.Invoke();
        }
    }
}
