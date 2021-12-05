using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public string text_Timer;
    private float time_start;
    private float time_current;
    private float time_Max = 5f;
    private bool isEnded;

    public void Reset()
    {
        
    }

    public void StartTimer()
    {
        Reset_Timer();
        //isEnded = false;
    }

    public string EndTimer()
    {
        End_Timer();
        return text_Timer;
    }

    private void Start()
    {
        Reset_Timer();
    }

    void Update()
    {
        if (isEnded)
            return;

        Check_Timer();
    }

    //CodeFinder
    //From https://codefinder.janndk.com/
    private void Check_Timer()
    {
        time_current = Time.time - time_start;
        if (time_current > -1)
        {
            text_Timer = $"{time_current:N2}";
            //Debug.Log(time_current);
            //Debug.Log(text_Timer);
        }
        else if (!isEnded)
        {
            End_Timer();
        }
    }

    private void End_Timer()
    {
        Debug.Log("End");
        
        text_Timer = $"{time_current:N2}";
        isEnded = true;
        Debug.Log(text_Timer);
    }


    private void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
        text_Timer = $"{time_current:N2}";
        isEnded = false;
        Debug.Log("Start");
    }
}
