using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;   // Arrastrá aquí tu panel del menú
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        menuUI.SetActive(true);   // Mostrar menú
        Time.timeScale = 0f;      // Pausar tiempo
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;      // Reanudar tiempo
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}