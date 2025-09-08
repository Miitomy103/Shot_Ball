using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
