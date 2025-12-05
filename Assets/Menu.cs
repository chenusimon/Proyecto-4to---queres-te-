using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject timer;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("dieOnMenu");
    }

    public void ToMenu()
    {
        Destroy(timer);

        SceneManager.LoadScene("SampleScene");
    }
}