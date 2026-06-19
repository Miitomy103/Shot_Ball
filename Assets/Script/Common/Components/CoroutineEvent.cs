using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ShotBall.InGame
{
    public class CoroutineEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent UnityEvent;
        [SerializeField] float waitTime = 1f;
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
