using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ShotBall.Create
{
    public class PointerDownHandler : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent onPointerDown = new UnityEvent();
        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke();
        }
    }
}
