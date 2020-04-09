using UnityEngine;
using System.Collections;
using System;

public class TrackSection : MonoBehaviour
{
    [SerializeField]
    private Section[] sections;
    public int actualSectionIndex;

    public event EventHandler OnStartFinishPass;
    private bool firstPass = true;

    void Awake()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].Enter += TrackSection_Enter;
            sections[i].sectionEnable = false;
        }

        sections[0].sectionEnable = true;
        actualSectionIndex = 0;
    }

    private void TrackSection_Enter(EventTrigger trigger, Collider other)
    {
        Section actualSection = trigger as Section;

        if (actualSection.isValidPass)
        {
            Debug.Log("Track Section Valid pass " + actualSection.gameObject.name);

            if (firstPass)
            {
                firstPass = false;

                actualSection.sectionEnable = false;

                actualSectionIndex++;

                sections[actualSectionIndex].sectionEnable = true;

                if (OnStartFinishPass != null)
                {
                    OnStartFinishPass(this, null);
                }
            }
            else
            {
                if (actualSectionIndex == 0)
                {
                    if (OnStartFinishPass != null)
                    {
                        OnStartFinishPass(this, null);
                    }
                }

                actualSection.sectionEnable = false;

                actualSectionIndex++;

                if (actualSectionIndex < sections.Length - 1)
                {

                }
                else
                {
                    actualSectionIndex = 0;
                }

                sections[actualSectionIndex].sectionEnable = true;
            }
        }
        else
        {
            //Está indo na direção errada
        }
    }
}
