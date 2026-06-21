using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 開始時にインスペクタで指定した有効/無効状態をこのGameObjectに適用するクラス。
/// </summary>
public class StartIsActive : MonoBehaviour
{
    [SerializeField] bool isActive = true;

    private void Start()
    {
        gameObject.SetActive(isActive);
    }
}
