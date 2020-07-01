using JazzDev.Easing;
using JazzDev.Executor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIAnimatableAlphaCanvasGroup : UIAnimatableAlphaBase
{
    [SerializeField]private bool changeInteractableState;

    protected CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
        originalAlphaValue = canvasGroup.alpha;
        executor.OnFinishEvent+=(executor)=>
        {
            if (changeInteractableState) 
            {
                canvasGroup.interactable = !canvasGroup.interactable; 
                canvasGroup.blocksRaycasts = canvasGroup.interactable;
            }
        };
    }
    protected override void OnExecutorUpdate(ExecEvaluation executor)
    {
        base.OnExecutorUpdate(executor);
        canvasGroup.alpha = Mathf.Lerp(originalAlphaValue,targetAlphaValue,evaluation);
    }
}