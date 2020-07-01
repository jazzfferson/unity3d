using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIAnimatableElementList", menuName = "ScriptableObjects/UI Animatable Element List", order = 1)]
public class UIAnimatableElementList_ScriptableObject : ObjectListAsset_ScriptableObject<UIAnimatableElementBase>
{
    public void PlayAll(bool reverse)
    {
        for (int i = 0; i < ElementsList.Count; i++)
        {
            ElementsList[i].PlayAnimation(reverse);
        }
    }

    public void PlayAllForward()
    {
        for (int i = 0; i < ElementsList.Count; i++)
        {
            ElementsList[i].PlayAnimation(false);
        }
    }

    public void PlayAllBackward()
    {
        for (int i = 0; i < ElementsList.Count; i++)
        {
            ElementsList[i].PlayAnimation(true);
        }
    }

    public void PlayAllSwitch()
    {
        for (int i = 0; i < ElementsList.Count; i++)
        {
            ElementsList[i].PlaySwitch();
        }
    }
}