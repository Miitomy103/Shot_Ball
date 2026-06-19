using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class SaveDataTesting : MonoBehaviour,IGet
    {
        [SerializeField]int maxStage = 4;

        [SerializeField] bool get;
        public bool IsGet => get;

        public void AllClear()
        {
            for (int i = 0; i <= maxStage; i++)
            {
                for(int j = 1; j <= 5; j++)
                {
                    DataSave.StageSave(new StageName(i, j), true);
                    DataSave.CoinSave(new StageName(i, j), new IGet[] { this, this, this });
                }
            }
        }
    }
}
