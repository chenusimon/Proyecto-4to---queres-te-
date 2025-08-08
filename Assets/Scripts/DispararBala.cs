using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararBala : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDeDisparo;

    public void Shoot(float xRotation)
    {

        // Crear la rotación en X (inclinación vertical)
        Quaternion rotacion = Quaternion.Euler(xRotation, 0, 0);

        // Instanciar la bala con la rotación especificada
        GameObject bala = Instantiate(prefabBala, puntoDeDisparo.position, rotacion);

        // Aplicar velocidad si la bala tiene Rigidbody
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bala.transform.forward * velocidad;
        }
    }
}

