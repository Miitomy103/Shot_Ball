using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RectTransformの変化を通知してほしいオブジェクトが実装するインターフェース。UIBehaviourComponentと組み合わせて使用する。
/// </summary>
public interface IUIBehaviour
{
    /// <summary>
    /// 親のRectTransformのサイズが変化したときに呼ばれる。
    /// </summary>
    void OnRectTransformChange();
}
