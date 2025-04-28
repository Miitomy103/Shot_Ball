using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Block_Data : GridDataBase
    {

        [SerializeField] string[] gridData = new string[] {"2", };

        public override int[,] GridData => GridDataUtil.FromTemplate(gridData);

        [SerializeField] bool center;
        public override bool Center => center;
    }
}
