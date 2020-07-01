using UnityEngine;

public class InterpolateUIIntMaterialProperty : InterpolateUIMaterialProperty<int>
{
    public override void UpdateValue(float t)
    {
        m_Material.SetInt(propertyName,Mathf.RoundToInt(Mathf.Lerp(startValue,endValue,t)));
    }
}
