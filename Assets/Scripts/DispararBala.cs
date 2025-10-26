using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararBala : MonoBehaviour
{
    public GameObject prefabBala;
    public GameObject prefabBala2;
    public GameObject prefabBala3;
    public Transform puntoDeDisparo;
    float velocidad = 40f;
    int arma = 1;
    int tiempo = 0;
    public Transform orientation;
    public Rigidbody rbPlayer;
    public GameObject player;
    float knockback = -40f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arma = 2;
            tiempo = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arma = 1;
            tiempo = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            arma = 3;
            tiempo = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(orientation.transform.eulerAngles.x , orientation.transform.eulerAngles.y, arma);
        }
    }

    void FixedUpdate()
    {
        tiempo += 1;
    }

    public void Shoot(float xRotation, float yRotation, int arma)
    {
        if (arma == 1) {
            if (tiempo >= 20) {
                Quaternion rotacion = Quaternion.Euler(xRotation, yRotation, 0);
                GameObject bala = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion);
                Rigidbody rb = bala.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bala.transform.forward * velocidad;
                }
                tiempo = 0;
            }
        }
        else if (arma == 2) {
            if (tiempo >= 50)
            {

                Quaternion rotacion1 = Quaternion.Euler(xRotation, yRotation += 10, 0);
                GameObject bala1 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion1);
                Rigidbody rb1 = bala1.GetComponent<Rigidbody>();
                if (rb1 != null)
                {
                    rb1.velocity = bala1.transform.forward * velocidad;
                }

                Quaternion rotacion2 = Quaternion.Euler(xRotation += 15, yRotation -= 5, 0);
                GameObject bala2 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion2);
                Rigidbody rb2 = bala2.GetComponent<Rigidbody>();
                if (rb2 != null)
                {
                    rb2.velocity = bala2.transform.forward * velocidad;
                }

                Quaternion rotacion3 = Quaternion.Euler(xRotation -= 7, yRotation += 9, 0);
                GameObject bala3 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion3);
                Rigidbody rb3 = bala3.GetComponent<Rigidbody>();
                if (rb3 != null)
                {
                    rb3.velocity = bala3.transform.forward * velocidad;
                }

                Quaternion rotacion4 = Quaternion.Euler(xRotation -= 2, yRotation -= 10, 0);
                GameObject bala4 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion4);
                Rigidbody rb4 = bala4.GetComponent<Rigidbody>();
                if (rb4 != null)
                {
                    rb4.velocity = bala4.transform.forward * velocidad;
                }

                Quaternion rotacion5 = Quaternion.Euler(xRotation += 8, yRotation, 0);
                GameObject bala5 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion5);
                Rigidbody rb5 = bala5.GetComponent<Rigidbody>();
                if (rb5 != null)
                {
                    rb5.velocity = bala5.transform.forward * velocidad;
                }
                rbPlayer.velocity = orientation.transform.forward * knockback;
                tiempo = 0;
            }
        }
        else if (arma == 3)
        {
            if (tiempo >= 100)
            {
                Quaternion rotacion = Quaternion.Euler(xRotation, yRotation, 0);
                GameObject bala = Instantiate(prefabBala3, puntoDeDisparo.position, rotacion);
                Rigidbody rb = bala.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bala.transform.forward * velocidad/2;
                }
                tiempo = 0;
            }
        }
    }
}

