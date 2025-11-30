using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class misilArea : MonoBehaviour
{
    public BossManagerScript BossManagerScript;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("jefe"))
        {
            BossManagerScript = collision.gameObject.GetComponent<BossManagerScript>();
            BossManagerScript.vida += -50;
        }
        Destroy(gameObject);
    }
}
