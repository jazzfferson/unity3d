using JazzDev.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIAnimatableElementParameter", menuName = "ScriptableObjects/UI Animatable Element List/Parameters", order = 1)]
public class UIAnimatedElementParameters : ScriptableObject
{
    [SerializeField]
    private UIAnimatableElementList_ScriptableObject animatableListManager;

    [SerializeField]
    private EaseStyle easeStyleForward = EaseStyle.StrongEaseIn;
    [SerializeField]
    private EaseStyle easeStylePlayBackward = EaseStyle.ExpoEaseOut;

    [SerializeField]
    private float backwardDealy = 0f;

    [SerializeField]
    private float forwardDelay = 0f;

    [SerializeField]
    private float forwardDuration = 0.25f;

    [SerializeField]
    private float backwardDuration = 0.25f;

   
    public UIAnimatableElementList_ScriptableObject AnimatableListManager { get => animatableListManager;}
    public EaseStyle EaseStyleForward { get => easeStyleForward;}
    public EaseStyle EaseStylePlayBackward { get => easeStylePlayBackward; }
    public float BackwardDealy { get => backwardDealy;}
    public float ForwardDelay { get => forwardDelay;}
    public float ForwardDuration { get => forwardDuration;}
    public float BackwardDuration { get => backwardDuration;}
}
