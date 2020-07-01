using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UnityUIButton:MonoBehaviour
{
    protected Button button;
    public Button Button { get => button;}
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }
}


