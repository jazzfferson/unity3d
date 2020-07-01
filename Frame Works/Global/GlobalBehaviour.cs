using UnityEngine;
using System.Collections;

public abstract class GlobalBehaviour<T> : MonoBehaviour where T: ScriptableObject
{
    [SerializeField]
    private T m_Global;
    public T Global
    {
        get
        {
            if (m_Global == null) {m_Global = Load(); Debug.LogWarning("Passe a referência do globalproperties no gameobject se possível");}
            return m_Global;
        }
    }
    private readonly string globalScriptObjectPath = "GlobalProperties";

    protected virtual void Awake()
    {
        if (m_Global == null) { m_Global = Load();}
    }
    private T Load()
    {
         return Resources.Load<T>(globalScriptObjectPath);
    }
}
