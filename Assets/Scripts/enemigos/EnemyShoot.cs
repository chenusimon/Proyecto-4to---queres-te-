using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDeDisparo;
    float velocidad = 40f;
    public Transform orientation;
    public AgentManager agentManager;

    public void Shoot(float xRotation, float yRotation, int arma)
    {

        Quaternion rotacion = Quaternion.Euler(xRotation, yRotation, 0);
        GameObject bala = Instantiate(prefabBala, puntoDeDisparo.position, rotacion);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bala.transform.forward * velocidad;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && agentManager.activated == false )
        {
            agentManager.activate();
        }
    }
}