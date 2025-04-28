using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] LayerMask ballLayer;

        [SerializeField] float areaRudius;

        // Update is called once per frame
        void Update()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, areaRudius, ballLayer);
            if (collider)
            {
                GameLoop.Instance.GetCoin();
                gameObject.SetActive(false);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            // 円（ワイヤーフレーム）
            GizmosUtility.DrawWireRegularPolygon(4, transform.position, areaRudius);
        }
    }
}
