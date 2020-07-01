using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public abstract class InterpolateUIMaterialProperty<T> : MonoBehaviour where T: struct
{
    protected Material m_Material;
    [SerializeField]protected string propertyName;
    [SerializeField]protected T startValue, endValue;

    protected virtual void Awake()
    {
        m_Material = GetComponent<Graphic>().material;
    }
    public abstract void UpdateValue(float t);
}