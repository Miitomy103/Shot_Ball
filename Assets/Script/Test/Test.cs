using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// デバッグ用の検証コード。本番では未使用。
/// </summary>
public class Test : MonoBehaviour
{
    public static int a;

    private void Update()
    {
        a++;
        IEnumerator enumerator;
    }
    IEnumerator Coroutine()
    {
        yield return new WaitForEndOfFrame();
    }
}
