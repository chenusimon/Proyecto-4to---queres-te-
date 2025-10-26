using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granada : MonoBehaviour
{
    public GameObject explosionEffect; // Prefab de partículas
    public float explosionRadius = 5f; // Radio más realista
    public float explosionForce = 700f; // Fuerza realista
    public float destroyEnemyDistance = 1f;

    private bool hasExploded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;
        hasExploded = true;

        // Efecto visual
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Detecta objetos cercanos
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Vector desde el centro de la explosión hacia el objeto
                Vector3 direction = (nearby.transform.position - transform.position).normalized;

                // Calcula distancia
                float distance = Vector3.Distance(transform.position, nearby.transform.position);
                float distanceFactor = Mathf.Clamp01(1 - (distance / explosionRadius));

                // Aplica fuerza dirigida proporcional a la cercanía
                rb.AddForce(direction * explosionForce * distanceFactor, ForceMode.Impulse);

                // Destruye enemigos cercanos
                if (distance < destroyEnemyDistance && nearby.CompareTag("enemigo"))
                {
                    Destroy(nearby.gameObject);
                }
            }
        }

        // Destruye la granada
        Destroy(gameObject, 0.1f); // pequeño delay para que el efecto se vea
    }
}
