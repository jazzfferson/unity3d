using JazzDev.Easing;
using JazzDev.Executor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class UIAnimatableElementBase : MonoBehaviour
{
    [SerializeField] protected UIAnimatedElementParameters parameters;

    [SerializeField] protected float forwardDelayAdjuster;
    [SerializeField] protected float backwardDelayAdjuster;

    [SerializeField] protected float forwardDurationAdjuster;
    [SerializeField] protected float backwardDurationAdjuster;


    [SerializeField] protected bool useSelfEaseStyle;
    [SerializeField] protected EaseStyle forwardEaseStyle;
    [SerializeField] protected EaseStyle backwardEaseStyle;


    [FormerlySerializedAs("onAnimationForwardEvent")]
    [SerializeField] protected UIAnimatableEvent onAnimationForwardFinish = new UIAnimatableEvent();
    [SerializeField] protected UIAnimatableEvent onAnimationForwardStart = new UIAnimatableEvent();

    [FormerlySerializedAs("onAnimationBackwardEvent")]
    [SerializeField] protected UIAnimatableEvent onAnimationBackwardFinish = new UIAnimatableEvent();
    [SerializeField] protected UIAnimatableEvent onAnimationBackwardStart = new UIAnimatableEvent();

    [FormerlySerializedAs("onUpdate")]
    [SerializeField] protected UIAnimatableUpdateEvent onUpdate = new UIAnimatableUpdateEvent();

    protected Ease ease;
    protected bool reverse = true;
    //private bool switchPlay = false;
    protected ExecEvaluation executor;
    public ExecEvaluation Executor { get => executor; }

    protected float evaluation;
    protected float Evaluation { get => evaluation; }

    protected virtual void Awake()
    {
        executor = new ExecEvaluation(EUpdateType.Normal).SetUpdate(OnExecutorUpdate)
            .SetDestroy(false)
            .SetId(gameObject.name)
            .SetStart(OnStart)
            .SetFinish(OnFinish);
    }

    protected virtual void OnStart(ExecEvaluation evaluator)
    {
        if (reverse) { onAnimationBackwardStart.Invoke(); }
        else { onAnimationForwardStart.Invoke(); }
    }

    protected virtual void OnFinish(ExecEvaluation evaluator)
    {
        if (reverse) { onAnimationBackwardFinish.Invoke(); }
        else { onAnimationForwardFinish.Invoke(); }
        //if (switchPlay) { reverse = !reverse; switchPlay = false; }
    }

    protected float GetAnimatedEvaluation(float executorEvaluation)
    {
        return EaseMethods.EasedLerp(ease, reverse ? 1f : 0f, reverse ? 0f : 1f, executorEvaluation);
    }
    private EaseStyle GetEaseStyle()
    {
        EaseStyle easeStyle = EaseStyle.Linear;

        if (useSelfEaseStyle)
        {
            easeStyle = reverse ? backwardEaseStyle : forwardEaseStyle;
        }
        else if (parameters != null)
        {
            easeStyle = reverse ? parameters.EaseStylePlayBackward : parameters.EaseStyleForward;
        }

        return easeStyle;
    }
    private float GetDelay()
    {
        float delay = 0;

        if (reverse)
        {
            if (parameters != null)
                delay = parameters.BackwardDealy;
            delay += backwardDelayAdjuster;
        }
        else
        {
            if (parameters != null)
                delay = parameters.ForwardDelay;
            delay += forwardDelayAdjuster;
        }

        return delay;
    }
    private float GetDuration()
    {
        float duration = 0;

        if (reverse)
        {
            if (parameters != null)
                duration = parameters.BackwardDuration;
            duration += backwardDurationAdjuster;
        }
        else
        {
            if (parameters != null)
                duration = parameters.ForwardDuration;
            duration += forwardDurationAdjuster;
        }

        return duration;
    }

    public virtual void PlayAnimation(bool reverse)
    {
        if(this.reverse == reverse)return;
        this.reverse = reverse;

        float currentEvaluation = executor.Evaluation;
        executor.SetDelay(GetDelay());
        executor.SetDuration(GetDuration());
        ease = EaseMethods.GetEase(GetEaseStyle());
       
        executor.Run();

        if(currentEvaluation>0 && currentEvaluation<1)
        executor.SetEvaluation(1-currentEvaluation);
    }
    public void PlayForward()
    {
        PlayAnimation(false);
    }
    public void PlayBackward()
    {
        PlayAnimation(true);
    }
    public void PlaySwitch()
    {
       // switchPlay = true;
        PlayAnimation(!reverse);
    }

    protected virtual void OnExecutorUpdate(ExecEvaluation executor)
    {
        evaluation = GetAnimatedEvaluation(executor.Evaluation);
        onUpdate.Invoke(evaluation);
    }
    protected void OnEnable()
    {
        if (parameters != null) parameters.AnimatableListManager?.AddElement(this);
    }

    protected void OnDisable()
    {
        if (parameters != null) parameters.AnimatableListManager?.RemoveElement(this);
    }

    [Serializable] public class UIAnimatableEvent : UnityEvent { }
    [Serializable] public class UIAnimatableUpdateEvent : UnityEvent<float> { }

    private void OnDestroy()
    {
        executor?.Destroy();
    }
}