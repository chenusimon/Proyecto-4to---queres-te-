using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public void UpdateTimerText(string time, string mins)
    {
        timer.text = mins + ":" + time;
    }
}
    