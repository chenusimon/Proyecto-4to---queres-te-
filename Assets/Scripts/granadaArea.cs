using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granadaArea : MonoBehaviour
{
    public BossManagerScript BossManagerScript;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("jefe"))
        {
            BossManagerScript = collision.gameObject.GetComponent<BossManagerScript>();
            BossManagerScript.vida += -30;
        }
        else if (collision.gameObject.CompareTag("enemigo")) ;
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
    }
