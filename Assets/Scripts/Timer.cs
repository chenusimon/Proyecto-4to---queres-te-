using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float mins = 0f;
    public float cuenta = 0f;
    public UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void FixedUpdate()
    {
        cuenta += Time.fixedDeltaTime;

        if (cuenta >= 60f)
        {
            mins += 1f;
            cuenta -= 60f;
        }

        uiManager.UpdateTimerText(cuenta.ToString("00"), mins.ToString("00"));
    }
}
