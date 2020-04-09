using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FrameUI : MonoBehaviour
{

    public TextMeshProUGUI m_primeiroLance,m_segundoLance, m_terceiroLance, m_resultadoParcial;
    private float primeiroLanceValue, segundoLanceValue, terceiroLanceValue, resultadoParcialValue;
    private List<TextMeshProUGUI> m_lances;
    private RectTransform m_rectTransform;
    public RectTransform rectTransform
    {
        get => m_rectTransform;
    }
    private CanvasGroup m_CanvasGroup;
    private int m_index;

    public void InitializeFrame(int index)
    {
        m_index = index;
        m_primeiroLance.text = "";
        m_segundoLance.text = "";
        if(m_terceiroLance!=null)
        m_terceiroLance.text = "";
        m_resultadoParcial.text = "";
        m_lances = new List<TextMeshProUGUI>();
        m_lances.Add(m_primeiroLance);
        m_lances.Add(m_segundoLance);
        m_lances.Add(m_terceiroLance);
        m_rectTransform = GetComponent<RectTransform>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0;
        float ySize = m_rectTransform.rect.height;
       /* Vector3 localPosition = m_rectTransform.transform.localPosition;
        m_rectTransform.transform.localPosition = new Vector3 (localPosition.x, ySize, localPosition.z);*/

    }

    public void SetStrike()
    {
        m_primeiroLance.text = "";
        m_segundoLance.text = "X";
     
    }
    public void SetSpare(int primeiroLance)
    {
        m_primeiroLance.text = primeiroLance.ToString();
        m_segundoLance.text = "/";

    }
    public void SetLance(int lance, int valor)
    {
        m_lances[lance].text = valor.ToString();
    }
    public void SetLance(int lance, string valor)
    {
        m_lances[lance].text = valor;
    }

    public void ResultadoParcial(int resultadoParcial)
    {      
        DOVirtual.Float(resultadoParcialValue, resultadoParcial, 0.25f,(float valor) => 
        {
            resultadoParcialValue = valor;
            m_resultadoParcial.text = Mathf.RoundToInt(resultadoParcialValue).ToString();
        });
    }
    public void Hide(float time = 0.32f)
    {
        float ySize = m_rectTransform.rect.height;
        m_rectTransform.transform.DOLocalMoveY(ySize, time);
    }
    public void Show(float time = 0.32f)
    {
        float ySize = m_rectTransform.rect.height;

        m_rectTransform.DOAnchorPosX(95 * m_index, 0.25f);
    }
    public void Visibility(float alpha, float time = 0.25f, float delayMultiplier = 0, TweenCallback onComplete = null)
    {
        m_CanvasGroup.DOFade(alpha, time).SetDelay(m_index * delayMultiplier).OnComplete(onComplete);
    }
  
}
