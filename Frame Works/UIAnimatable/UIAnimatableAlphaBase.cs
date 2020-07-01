using JazzDev.Easing;
using JazzDev.Executor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIAnimatableAlphaBase : UIAnimatableElementBase
{
    protected float originalAlphaValue;
    [SerializeField] protected Float_ScriptableObject targetValueScriptObj;
    [SerializeField] protected float targetValueOffset;
    protected float targetAlphaValue;

    public override void PlayAnimation(bool reverse)
    {
         targetAlphaValue = targetValueOffset;
        if (targetValueScriptObj != null) {targetAlphaValue +=  targetValueScriptObj.Value;}
        base.PlayAnimation(reverse);
    }
}
