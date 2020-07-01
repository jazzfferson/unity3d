using UnityEngine;

public class InterpolateUIFloatMaterialProperty : InterpolateUIMaterialProperty<float>
{
    public override void UpdateValue(float t)
    {
        m_Material.SetFloat(propertyName,Mathf.Lerp(startValue,endValue,t));
    }
}
