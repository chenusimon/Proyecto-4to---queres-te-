using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalaEnemyScript : MonoBehaviour
{


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("bala") || collision.gameObject.CompareTag("enemigo"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}