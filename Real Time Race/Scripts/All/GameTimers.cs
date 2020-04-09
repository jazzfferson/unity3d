using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameTimers : MonoBehaviour
{

    TrackReferences trackReferences;
    bool[] sectionsCompleted;
    EventTrigger finishTrigger, nextSession;
    public Text totalTime, bestLap, lastLap, actualLap, lapCounter, maxLapsCounter;
    float totalTimeCounter, bestLapCounter, lastLapCounter, actualLapCounter;
    int laps = 0;
    public int maxLaps;
    bool running = false;
    bool finished = false;

    public int sections, actualSection;
    public event EventHandler OnNewBestTime, OnFinishedLap, OnRaceStarted, OnRaceEnded;

    void Start()
    {
        StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);
        OnFinishedLap += new EventHandler(GameTimers_OnFinishedLap);
    }

    void GameTimers_OnFinishedLap(object sender, EventArgs e)
    {
        StaticReferences.Instance.car.GetComponent<CarReplay>().SetLapInfo(lastLapCounter);
    }

    void Instance_OnReferencesReady()
    {
        maxLapsCounter.text = "/" + maxLaps.ToString();
        lapCounter.text = laps.ToString();
        bestLap.text = StringTime(0);
        lastLap.text = StringTime(0);
        actualLap.text = StringTime(0);
        totalTime.text = StringTime(0);
        trackReferences = StaticReferences.Instance.pista.GetComponent<TrackReferences>();
        finishTrigger = trackReferences.finishTrigger;
        finishTrigger.Enter += new EventTrigger.EventTriggerDelegate(finishTrigger_Enter);
        StartCoroutine(customLoop(0.07f));
        sections = trackReferences.sessoes.Length;
        sectionsCompleted = new bool[sections];
        actualSection = 0;
        foreach (EventTrigger trigger in trackReferences.sessoes)
        {
            trigger.Enter += new EventTrigger.EventTriggerDelegate(trigger_Enter);
        }

    }

    void trigger_Enter(string Name, int[] IDs)
    {
        SectionPass(IDs[0]);
    }

    void finishTrigger_Enter(string Name, int[] IDs)
    {


        if (!running && !finished)
        {
            StartRace();
            running = true;
        }
        else if (running && !finished && AllSectionsCompleted())
        {
            ResetLapCounter();
        }
    }

    void Timers()
    {
        totalTime.text = StringTime(totalTimeCounter);
        actualLap.text = StringTime(actualLapCounter);
    }

    string StringTime(float time)
    {

        int minuto = (int)((time / 60) % 60);
        float segundos = Mathf.Repeat(time, 60);
        return String.Concat(minuto.ToString(), "'", String.Format("{0:0.000}", segundos));
    }

    void Update()
    {
        if (running)
        {
            float deltaTime = Time.deltaTime;
            totalTimeCounter += deltaTime;
            actualLapCounter += deltaTime;
        }



    }
    void ResetLapCounter()
    {
        print("Reset Lap Counter");
        ResetSessions();



        if (laps < maxLaps)
        {
            laps++;
            lapCounter.text = laps.ToString();

            if (OnFinishedLap != null)
            {
                OnFinishedLap(this, null);
            }

        }
        else
        {
            EndRace();
        }

        if (actualLapCounter < bestLapCounter || bestLapCounter == 0)
        {
            bestLapCounter = actualLapCounter;

            if (OnNewBestTime != null)
            {
                OnNewBestTime(this, null);
            }
        }



        lastLapCounter = actualLapCounter;
        actualLapCounter = 0;

        lastLap.text = StringTime(lastLapCounter);
        bestLap.text = StringTime(bestLapCounter);

    }
    void StartRace()
    {
        print("Race Started!!");
        laps++;
        lapCounter.text = laps.ToString();

        if (OnRaceStarted != null)
        {
            OnRaceStarted(this, null);
        }

    }
    void EndRace()
    {
        print("Race Ended!!");
        finished = true;
        running = false;
        finishTrigger.gameObject.SetActive(false);
        actualLap.text = StringTime(0);
        if (OnRaceEnded != null)
        {
            OnRaceEnded(this, null);
        }

    }
    IEnumerator customLoop(float delay)
    {
        yield return new WaitForSeconds(delay);
        Timers();
        StartCoroutine(customLoop(delay));
    }
    bool AllSectionsCompleted()
    {
        foreach (bool sessao in sectionsCompleted)
        {
            if (!sessao)
            {
                return false;
            }
        }
        return true;
    }
    void SectionPass(int session)
    {

        if (sectionsCompleted[session])
        {
            Debug.Log("Sessão = " + session + "  Despassada");
            sectionsCompleted[session] = false;
        }
        else
        {
            Debug.Log("Sessão = " + session + "  Passada");
            sectionsCompleted[session] = true;
        }

        if (AllSectionsCompleted())
            Debug.Log("Passou por todas as sessões");


    }
    void ResetSessions()
    {
        for (int i = 0; i < sectionsCompleted.Length; i++)
        {
            sectionsCompleted[i] = false;
        }
    }


}
