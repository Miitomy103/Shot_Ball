using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class LaunchPad : MonoBehaviour
    {
        [SerializeField] GameObject ball;

        Ball Ball;
        private void Start()
        {
            GameObject b = Instantiate(ball, transform.position, Quaternion.identity);
            Ball = b.GetComponent<Ball>();
        }
        public void GameStart()
        {
            Debug.Log("Start");
            Ball.PlayStart();
        }
        public void GameReset()
        {
            if(Ball!=null)
            {
                Destroy(Ball.gameObject);
                Ball = null;
            }
            GameObject b = Instantiate(ball, transform.position, Quaternion.identity);
            Ball = b.GetComponent<Ball>();
        }
    }
}
