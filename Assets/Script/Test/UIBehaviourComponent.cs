using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// RectTransformのサイズ変化を子のIUIBehaviour実装に伝播させるクラス。
/// </summary>
public class UIBehaviourComponent : UIBehaviour
{
    protected override void OnRectTransformDimensionsChange()
    {

        Debug.Log("OnRectTransformChange");
        base.OnRectTransformDimensionsChange();
        IUIBehaviour[] uIBehaviours = GetComponentsInChildren<IUIBehaviour>();
        foreach(var u in uIBehaviours)
        {
            u.OnRectTransformChange();
        }
    }
}
