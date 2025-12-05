using System;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;
    private bool isPaused = false;
    bool pausable = true;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pausable)
            {
                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }

    void Start()
    {
        menuUI.SetActive(false);
    }

    public void PauseGame()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
