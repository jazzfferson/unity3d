using UnityEngine;
using System.Collections;

public class AnimaCurvePreset : MonoBehaviour {

    [SerializeField]
    private AnimationCurve _animationCurvePreset;
    [SerializeField]
    private string _name;
    public AnimationCurve AnimationCurvePreset
    {
        get { return _animationCurvePreset; }
    }
    public string Name
    {
        get { return _name; }
    }

}
