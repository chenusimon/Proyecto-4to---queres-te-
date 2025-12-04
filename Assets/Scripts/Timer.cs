using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int mins = 0;
    public int ms = 0;
    public int secs = 0;
    public UIManager uiManager;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void FixedUpdate()
    {
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        ms += 2;

        if (ms >= 100f)
        {
            secs += 1;
            ms -= 100;
        }
        if (secs >= 60)
        {
            mins += 1;
            secs -= 60;
        }

        uiManager.UpdateTimerText(secs.ToString("00"), mins.ToString("00"), ms.ToString("00"));
    }
}
