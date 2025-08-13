using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararBala : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDeDisparo;
    float velocidad = 40f;


    public void Shoot(float xRotation, float yRotation)
    {
        Quaternion rotacion = Quaternion.Euler(xRotation, yRotation, 0);        
        GameObject bala = Instantiate(prefabBala, puntoDeDisparo.position, rotacion);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bala.transform.forward * velocidad;
        }
    }
}

