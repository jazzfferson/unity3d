using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(TMP_Text))]
public class UITextTypeWriterEffector : MonoBehaviour, IPausable<UITextTypeWriterEffector>
{

    [SerializeField] private float minTypingDelay = 0.01f;
    [SerializeField] private float maxTypingDelay = 0.04f;
    [SerializeField] private int charsTypedToCallEvent = 4;
    [SerializeField] private UITMPTypeWriterEffectorEvent onNewCharTyped = new UITMPTypeWriterEffectorEvent();
    [SerializeField] private UITMPTypeWriterEffectorEvent onFinished = new UITMPTypeWriterEffectorEvent();

    private TMP_Text m_TextComponent;
    private bool ended = true;
    private float timer;
    private float delay;
    private bool pause;
    private int currentChar = 0;
    private bool noTextToPerform;

    public TMP_Text TextPro { get => m_TextComponent; }
    public UITMPTypeWriterEffectorEvent OnNewCharTyped { get => onNewCharTyped; }
    public UITMPTypeWriterEffectorEvent OnFinished { get => onFinished; }

    public event Action<UITextTypeWriterEffector> OnPauseChangeCallBack;
    public bool Pause { get => pause; set { pause = value; OnPauseChangeCallBack?.Invoke(this); } }

    void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
        SetInitialState();
        Debug.Log("Awaked");
    }
    private void Update()
    {
        if (ended || pause || noTextToPerform) return;

        timer += Time.deltaTime;

        if (timer >= delay)
        {
            timer = 0;
            delay = UnityEngine.Random.Range(minTypingDelay, maxTypingDelay);

            m_TextComponent.maxVisibleCharacters = currentChar;

            if (currentChar % charsTypedToCallEvent == 0) {onNewCharTyped.Invoke();}
            
            currentChar++;
           
            if (currentChar >  m_TextComponent.textInfo.characterCount) { ended = true; onFinished.Invoke(); }
        }
    }

    public void Begin()
    {
       // if(noTextToPerform)return;
        SetInitialState();
        onNewCharTyped.Invoke();
        ended = false;
    }
    public void BeginDelayed(float delay=0)
    {
       if(noTextToPerform)return;
       SetInitialState();
       StartCoroutine(BeginDelayedCoroutine(delay));
    }

    private IEnumerator BeginDelayedCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        onNewCharTyped.Invoke();
        ended = false;
    }

    private bool CheckText()
    {
        return noTextToPerform = string.IsNullOrEmpty(m_TextComponent.text);
    }

    public void SetInitialState()
    {
        ended = true;
        Pause = false;
        timer = 0;
        delay = 0;
        currentChar = 1;

         m_TextComponent.maxVisibleCharacters = 0;

        //if(CheckText())return;

      //  m_TextComponent.ForceMeshUpdate(true);
        
       /* for (int i = 0; i <  m_TextComponent.textInfo.characterCount; i++)
        {
            SetTextColor(i, 0);
        }*/
    }

    public void ForceEnd()
    {
       // if(noTextToPerform)return;
        ended = true;
        Pause = false;
        timer = 0;
        delay = 0;
        currentChar = 1;
       // m_TextComponent.ForceMeshUpdate(true);

         m_TextComponent.maxVisibleCharacters = m_TextComponent.textInfo.characterCount;
        /*for (int i = 0; i <  m_TextComponent.textInfo.characterCount; i++)
        {
            SetTextColor(i, 255);
        }*/
        onFinished.Invoke();
    }


    //int characterCount = textInfo.characterCount;
    private void SetTextColor(int charIndex, byte alpha)
    {

       
        // Skip characters that are not visible
      //  if (!textInfo.characterInfo[charIndex].isVisible) return;

      /*  Color32[] newVertexColors;

        // Get the index of the material used by the current character.
        int materialIndex =  m_TextComponent.textInfo.characterInfo[charIndex].materialReferenceIndex;

        // Get the vertex colors of the mesh used by this text element (character or sprite).
        newVertexColors =  m_TextComponent.textInfo.meshInfo[materialIndex].colors32;

        // Get the index of the first vertex used by this text element.
        int vertexIndex =  m_TextComponent.textInfo.characterInfo[charIndex].vertexIndex;

        // Set new alpha values.
        newVertexColors[vertexIndex + 0].a = alpha;
        newVertexColors[vertexIndex + 1].a = alpha;
        newVertexColors[vertexIndex + 2].a = alpha;
        newVertexColors[vertexIndex + 3].a = alpha;

        // Upload the changed vertex colors to the Mesh.
        m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);*/
    }

    [Serializable] public class UITMPTypeWriterEffectorEvent : UnityEvent { }
}


