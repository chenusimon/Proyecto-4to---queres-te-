using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalaScript : MonoBehaviour
{
    public BossManagerScript bossManager;


    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("bala") || collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else if (collision.gameObject.CompareTag("jefe"))
        {
            bossManager = collision.gameObject.GetComponent<BossManagerScript>();
            bossManager.vida += -1;
            Destroy(gameObject);
        }
        else
        {
        Destroy(gameObject);
        }
    }
}