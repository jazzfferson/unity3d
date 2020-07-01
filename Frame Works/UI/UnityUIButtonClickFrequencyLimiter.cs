using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityUIButtonClickFrequencyLimiter : UnityUIButton
{
    [SerializeField] protected float delayBetweenClicks = 1;
    protected override void Awake()
    {
        base.Awake();
        button.onClick.AddListener(() => {StartCoroutine(EnableButton());});
    }
    private IEnumerator EnableButton()
    {
        button.enabled = false;
        yield return new WaitForSeconds(delayBetweenClicks);
        button.enabled = true;
    }
}
