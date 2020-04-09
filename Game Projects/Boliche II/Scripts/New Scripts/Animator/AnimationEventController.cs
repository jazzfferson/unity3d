using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationEventController : MonoBehaviour
{

    private Animator m_animator;
    private AnimationClip[] clipsArray;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        clipsArray = m_animator.runtimeAnimatorController.animationClips;
        CreateEndEvent();
    }

    private void CreateEndEvent()
    {
        foreach (AnimationClip clip in clipsArray)
        {
            AnimationEvent animEvent = new AnimationEvent();
            animEvent.time = clip.length;
            animEvent.functionName = "RecieveEvent";
            animEvent.stringParameter = clip.name;
            clip.AddEvent(animEvent);
        }
    }
    public void RecieveEvent(string nome)
    {
        if (OnEndAnimationDelegate != null)
            OnEndAnimationDelegate(m_animator.GetCurrentAnimatorStateInfo(0), nome);

        if (OnEndAnimationEvent != null)
            OnEndAnimationEvent(m_animator.GetCurrentAnimatorStateInfo(0), nome);
    }
    public delegate void AnimationControllerEventHandler(AnimatorStateInfo animatorClipInfo, string animationClipName);
    public event AnimationControllerEventHandler OnEndAnimationEvent;
    public AnimationControllerEventHandler OnEndAnimationDelegate;
}
