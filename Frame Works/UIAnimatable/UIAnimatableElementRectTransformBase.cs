using JazzDev.Easing;
using JazzDev.Executor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public abstract class UIAnimatableElementRectTransformBase : UIAnimatableElementBase
{
    public enum EAxisFilter { X, Y, Z, XY, XZ, YZ, XYZ };

    [SerializeField] protected Vector3_ScriptableObject targetValueScriptObj;
    [SerializeField] protected bool useTargetTransformValue;
    [SerializeField] protected RectTransform targetRectTransformValue;
    [SerializeField] protected Vector3 targetValueOffset;

    [SerializeField] protected EAxisFilter axisFilter = EAxisFilter.XYZ;

    protected Vector3 filteredTargetValue;
    protected Vector3 originalTransformValue;

    protected LayoutGroup layout;
    protected RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
        layout = transform.parent.GetComponent<LayoutGroup>();
    }

    protected virtual IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        DisableParentLayout();
    }

    /// <summary>
    /// Tem que ser chamado depois do DisableParentLayout
    /// </summary>
    protected virtual void CalculateAxisFilter()
    {
        Vector3 targetValue =  GetTargetValue() + targetValueOffset;

        switch (axisFilter)
        {
            case EAxisFilter.X:
                filteredTargetValue = new Vector3(targetValue.x, originalTransformValue.y, originalTransformValue.z);
                break;
            case EAxisFilter.Y:
                filteredTargetValue = new Vector3(originalTransformValue.x, targetValue.y, originalTransformValue.z);
                break;
            case EAxisFilter.Z:
                filteredTargetValue = new Vector3(originalTransformValue.x, originalTransformValue.y, targetValue.z);
                break;
            case EAxisFilter.XY:
                filteredTargetValue = new Vector3(targetValue.x, targetValue.y, originalTransformValue.z);
                break;
            case EAxisFilter.XZ:
                filteredTargetValue = new Vector3(targetValue.x, originalTransformValue.y, targetValue.z);
                break;
            case EAxisFilter.YZ:
                filteredTargetValue = new Vector3(originalTransformValue.y, targetValue.y, targetValue.z);
                break;
            case EAxisFilter.XYZ:
                filteredTargetValue = targetValue;
                break;
        }
    }
    protected abstract Vector3 GetTargetValue();


    private void DisableParentLayout()
    {
        if (layout != null) { layout.enabled = false; }
    }
}







