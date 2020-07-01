using UnityEngine;

public class InterpolateUIColorMaterialProperty : InterpolateUIMaterialProperty<Color>
{
    public override void UpdateValue(float t)
    {
        m_Material.SetColor(propertyName, Color.Lerp(startValue,endValue,t));
    }
}
