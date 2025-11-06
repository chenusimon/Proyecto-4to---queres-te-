using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MisilScript : MonoBehaviour
{
    public Rigidbody rb;
    public Transform targetTR;
    public GameObject explosionEffect;
    public float explosionRadius = 5f;
    float explosionForce = 2000f;
    public float destroyEnemyDistance = 1f;
    private bool hasExploded = false;
    int cuenta = 0;

    void Awake()
    {
        targetTR = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        Vector3 direction = (targetTR.position - transform.position).normalized;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (cuenta >= 10)
        {
            rb.AddForce(transform.forward * 1000f, ForceMode.Acceleration);
            cuenta = 0;
        }
    }

    void FixedUpdate()
    {
        cuenta += 1;

    }


    public void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;
        hasExploded = true;

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (nearby.transform.position - transform.position).normalized;

                float distance = Vector3.Distance(transform.position, nearby.transform.position);
                float distanceFactor = Mathf.Clamp01(1 - (distance / explosionRadius));

                rb.AddForce(direction * explosionForce * distanceFactor, ForceMode.Impulse);

                if (distance < destroyEnemyDistance && nearby.CompareTag("enemigo"))
                {
                    Destroy(nearby.gameObject);
                }
            }
        }

        Destroy(gameObject, 0.1f);
    }
}