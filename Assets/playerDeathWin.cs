using UnityEngine;

public class playerDeathWin : MonoBehaviour
{
    [Header("Pantallas UI")]
    public GameObject deathScreen;
    public GameObject winScreen;




    void Start()
    {
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bala enemiga"))
        {
            ShowDeath();
        }
        else if (collision.gameObject.CompareTag("te"))
        {
            ShowWin();
        }
    }

    private void ShowDeath()
    {
        if (deathScreen != null)
            deathScreen.SetActive(true);

        FreezeGame();
    }

    private void ShowWin()
    {
        if (winScreen != null)
            winScreen.SetActive(true);

        FreezeGame();
    }

    private void FreezeGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
