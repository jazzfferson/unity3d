using JazzDev.Easing;
using JazzDev.Executor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UIAnimatableAlphaCanvasRenderer : UIAnimatableAlphaBase
{
    protected CanvasRenderer canvasRenderer;

    protected override void Awake()
    {
        base.Awake();
        canvasRenderer = GetComponent<CanvasRenderer>();
        originalAlphaValue = canvasRenderer.GetAlpha();
    }
    protected override void OnExecutorUpdate(ExecEvaluation executor)
    {
        base.OnExecutorUpdate(executor);
        canvasRenderer.SetAlpha(Mathf.Lerp(originalAlphaValue,targetAlphaValue,evaluation));
    }
}