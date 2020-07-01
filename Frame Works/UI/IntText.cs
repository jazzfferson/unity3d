using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class IntText : MonoBehaviour
{
    TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();    
    }
    public void SetText(int text)
    {
        textComponent.SetText(text.ToString());
    }
}
