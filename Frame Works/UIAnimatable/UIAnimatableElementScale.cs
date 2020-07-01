using JazzDev.Easing;
using JazzDev.Executor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIAnimatableElementScale : UIAnimatableElementRectTransformBase
{

    protected override IEnumerator Start()
    {
        yield return base.Start();
        originalTransformValue = rectTransform.localScale;
        CalculateAxisFilter();
    }

    protected override void OnExecutorUpdate(ExecEvaluation executor)
    {
        base.OnExecutorUpdate(executor);
        rectTransform.localScale = Vector3.Lerp(originalTransformValue, filteredTargetValue, evaluation);
    }

    protected override Vector3 GetTargetValue()
    {
        if (targetValueScriptObj != null && !useTargetTransformValue) { return targetValueScriptObj.Value; }
        else if (targetRectTransformValue != null) { return targetRectTransformValue.localScale; }
        else return Vector3.zero;
    }
}
