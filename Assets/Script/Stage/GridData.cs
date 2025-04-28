using ShotBall.InGame;
using UnityEngine;

public abstract class GridDataBase : MonoBehaviour, IGridData
{
    //0 => 空
    //1 => マス
    //2 => 中心地
    public abstract int[,] GridData { get; }
    public abstract bool Center { get; }

    private void Awake()
    {
        if (GridData == null) Debug.Log("gridDataない！！");
        if (Center)
        {
            bool centerCheck = false;
            foreach (int i in GridData)
            {
                if (i == 2 && centerCheck)
                {
                    centerCheck = false;
                    break;
                }
                if (i == 2)
                {
                    centerCheck = true;
                }
            }
            if (!centerCheck) Debug.LogError($"{this.GetType().Name}: 中心地が存在しません");
        }
        if(!Center)
        {
            foreach(int i in GridData)
            {
                if(i==3)
                {
                    foreach (int j in GridData)
                    {
                        if(j!=1||j!=2)
                        {
                            Debug.LogError("当たり判定なしブロックに当たり判定があります");
                        }
                    }
                    break;
                }
            }
        }
    }
}
