using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ShotBall.Create
{
    public class PointerClickHandler : MonoBehaviour,IPointerClickHandler
    {
        public UnityEvent onClick= new UnityEvent();
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}
