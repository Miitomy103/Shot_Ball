using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public abstract class ObjectBase : MonoBehaviour
    {
        public int BlockId { get; set; }
        protected abstract bool IsOverLapping();
        public bool PublicOverLapping() => IsOverLapping();
    }
}
