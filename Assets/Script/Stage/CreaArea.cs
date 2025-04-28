using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CreaArea : MonoBehaviour
    {
        [SerializeField] LayerMask ballLayer;

        [SerializeField] float areaRudius;

        // Update is called once per frame
        void Update()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, areaRudius,ballLayer);
            if (collider)
            {
                Ball ball =collider.gameObject.GetComponent<Ball>();
                ball.Clear();
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            // 円（ワイヤーフレーム）
            GizmosUtility.DrawWireCircle(transform.position, areaRudius);
        }
    }
}

