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

            coins=GetComponentsInChildren<Coin>().Cast<IGet>().ToArray();
        }
        private void Start()
        {
            GameLoop.Instance.CoinSet(coins);
        }
    }
}
