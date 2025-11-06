using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalaScript : MonoBehaviour
{


    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("bala") || collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}