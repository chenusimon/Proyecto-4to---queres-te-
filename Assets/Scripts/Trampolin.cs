using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    public PlayerMovement jumper;

    private void OnTriggerEnter
        (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jumper.Jump(4000f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jumper.Jump(4000f);
        }
    }
}