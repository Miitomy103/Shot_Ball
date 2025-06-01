using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShotBall.InGame
{
    public class CoinManager : MonoBehaviour
    {
        IGet[] coins = new IGet[3];
        private void Awake()
        {
            for(int i=0;i<transform.childCount;i++)
            {
                coins[i]= transform.GetChild(i).GetComponent<IGet>();
            }

        }
        private void Start()
        {
            GameLoop.Instance.CoinSet(coins);
        }
    }
}
