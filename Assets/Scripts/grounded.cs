using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounded : MonoBehaviour
{
    public BossManagerScript BossManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
         BossManager.grounded = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            BossManager.grounded = false;
        }
    }
}