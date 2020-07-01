using JazzDev.Easing;
using JazzDev.Executor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIAnimatableElementPosition : UIAnimatableElementRectTransformBase
{

    protected override IEnumerator Start()
    {
        yield return base.Start();
        originalTransformValue = rectTransform.anchoredPosition3D;
        CalculateAxisFilter();
    }

    protected override void OnExecutorUpdate(ExecEvaluation executor)
    {
        base.OnExecutorUpdate(executor);
        rectTransform.anchoredPosition3D = Vector3.Lerp(originalTransformValue, filteredTargetValue, evaluation);
    }

    protected override Vector3 GetTargetValue()
    {
        if (targetValueScriptObj != null && !useTargetTransformValue) {return targetValueScriptObj.Value;}
        else if(targetRectTransformValue!=null){return rectTransform.InverseTransformPoint(targetRectTransformValue.position);}
        else return Vector3.zero;
    }
}
