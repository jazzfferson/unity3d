using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public virtual void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this as T;
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("_gamemanager");
                    DontDestroyOnLoad(go.gameObject);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}