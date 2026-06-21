using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// ブロックを設置できないエリアを示すギミック。プレイ中のみコライダーがトリガーになり、ボールが通過できる。
    /// </summary>
    public class NoPutArea :GimmickBase
    {

        Collider2D colli;

        protected override void Awake()
        {
            base.Awake();
            colli = GetComponent<Collider2D>();
            colli.isTrigger = false;
        }
        protected override void Start()
        {
            base.Start();
            ThisSprite.sortingOrder = (int)OrderInLayer.NoputArea;
        }

        protected override void StageStart()
        {
            base.StageStart();
            colli.isTrigger = true;
        }
        protected override void StageReset()
        {
            base.StageReset();
            colli.isTrigger = false;
        }

        public override void LoadData(StageBlockData data)
        {

        }
    }
}
