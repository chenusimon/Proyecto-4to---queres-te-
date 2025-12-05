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
    float knockback = -10f;
    public MeshRenderer mr1;
    public MeshRenderer mr2;
    public MeshRenderer mr3;

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!(arma == 2))
            {
                arma = 2;
                tiempo = 0;
                mr2.enabled = true;
                mr1.enabled = false;
                mr3.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!(arma == 1))
            {
                arma = 1;
                tiempo = 0;
                mr1.enabled = true;
                mr2.enabled = false;
                mr3.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!(arma == 3))
            {
                arma = 3;
                tiempo = 0;
                mr3.enabled = true;
                mr1.enabled = false;
                mr2.enabled = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(orientation.transform.eulerAngles.x, orientation.transform.eulerAngles.y, arma);
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0f)
            return;

        tiempo += 1;
    }

    public void Shoot(float xRotation, float yRotation, int arma)
    {
        if (Time.timeScale == 0f)
            return;

        if (arma == 1)
        {
            if (tiempo >= 20)
            {
                Quaternion rotacion = Quaternion.Euler(xRotation, yRotation, 0);
                GameObject bala = Instantiate(prefabBala, puntoDeDisparo.position, rotacion);
                Rigidbody rb = bala.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 playerVelHorizontal = new Vector3(rbPlayer.velocity.x, 0, rbPlayer.velocity.z);
                    rb.velocity = (bala.transform.forward * velocidad) + playerVelHorizontal;
                }
                tiempo = 0;
            }
        }
        else if (arma == 2)
        {
            if (tiempo >= 70)
            {
                Quaternion rotacion1 = Quaternion.Euler(xRotation, yRotation += 4, 0);
                GameObject bala1 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion1);
                Rigidbody rb1 = bala1.GetComponent<Rigidbody>();
                if (rb1 != null)
                {
                    rb1.velocity = (bala1.transform.forward * velocidad) + rbPlayer.velocity;
                }

                Quaternion rotacion2 = Quaternion.Euler(xRotation -= 5, yRotation -= 4, 0);
                GameObject bala2 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion2);
                Rigidbody rb2 = bala2.GetComponent<Rigidbody>();
                if (rb2 != null)
                {
                    rb2.velocity = (bala2.transform.forward * velocidad) + rbPlayer.velocity;
                }

                Quaternion rotacion3 = Quaternion.Euler(xRotation += 5, yRotation, 0);
                GameObject bala3 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion3);
                Rigidbody rb3 = bala3.GetComponent<Rigidbody>();
                if (rb3 != null)
                {
                    rb3.velocity = (bala3.transform.forward * velocidad) + rbPlayer.velocity;
                }

                Quaternion rotacion4 = Quaternion.Euler(xRotation -= 2, yRotation -= 2, 0);
                GameObject bala4 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion4);
                Rigidbody rb4 = bala4.GetComponent<Rigidbody>();
                if (rb4 != null)
                {
                    rb4.velocity = (bala4.transform.forward * velocidad) + rbPlayer.velocity;
                }

                Quaternion rotacion5 = Quaternion.Euler(xRotation += 2, yRotation += 2, 0);
                GameObject bala5 = Instantiate(prefabBala2, puntoDeDisparo.position, rotacion5);
                Rigidbody rb5 = bala5.GetComponent<Rigidbody>();
                if (rb5 != null)
                {
                    rb5.velocity = (bala5.transform.forward * velocidad);
                }
                rbPlayer.velocity += orientation.transform.forward * knockback;
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
                    rb.velocity = (bala.transform.forward * velocidad / 2);
                }
                tiempo = 0;
            }
        }
    }
}
