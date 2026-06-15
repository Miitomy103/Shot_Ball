using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// コインの数をカウントし、UIに表示するクラス
    /// </summary>
    public class CoinCount : MonoBehaviour
    {
        public int coinCount = 0; // コインのカウント

        const int maxCoinCount = 3; // 最大コイン数

        [SerializeField] TextMeshProUGUI coinCountText; // コインのカウントを表示するUI

        Objects objects;
        private void Awake()
        {
            objects = GetComponent<Objects>();
        }
        private void Start()
        {
            CheckCoin(); // 初期状態でコインの数をチェック
            objects.UpdateObjects += CheckCoin; // オブジェクトが更新されるたびにコインの数をチェック
        }
        public void CheckCoin()
        {
            coinCount = 0;
            foreach(var obj in objects.CreateObjects)
            {
                if (obj.StageBlockData.Type == BlockType.Coin)
                {
                    coinCount++;
                }
            }
            TextChange(); // UIのテキストを更新
        }

        public void TextChange()
        {
            coinCountText.text = coinCount.ToString() + $"/{maxCoinCount}";
            if (coinCount != maxCoinCount)
            {
                coinCountText.color = Color.red; // 3個未満なら赤色
            }
            else
            {
                coinCountText.color = Color.white; // 3個なら緑色
            }
        }

        /// <summary>
        /// コインの数が3個であるかどうかを返すメソッド
        /// </summary>
        /// <returns></returns>
        public bool IsCoinCountValid()
        {
            return coinCount == maxCoinCount; // コインの数が3個であるかどうかを返す
        }
    }
}
