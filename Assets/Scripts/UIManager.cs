using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timer;

    void update ()
    {
        if (timer = null)
        {
            timer = GameObject.FindGameObjectWithTag("timer").GetComponent<TextMeshProUGUI>();
        }
    }
    public void UpdateTimerText(string time, string mins, string ms)
    {

        timer.text = mins + ":" + time + "," + ms;
    }
}
    