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
    float explosionForce = 200f;
    public float destroyEnemyDistance = 1f;
    private bool hasExploded = false;
    int cuenta = 0;
    public BossManagerScript bossManager;

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


    void OnCollisionEnter(Collision collision)
    {

        Destroy(gameObject, 0.1f);
        if (hasExploded) return;
        hasExploded = true;


        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearby in colliders)
        {
            if ((nearby.gameObject.CompareTag("enemigo")) || nearby.gameObject.CompareTag("jefe"))
            {
                NavMeshAgent agent = nearby.gameObject.GetComponent<NavMeshAgent>();
                agent.enabled = false;
                Debug.Log(agent.enabled);
                Rigidbody agentrb = nearby.GetComponent<Rigidbody>();
                agentrb.isKinematic = false;
            }
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (nearby.transform.position - transform.position).normalized;

                float distance = Vector3.Distance(transform.position, nearby.transform.position);
                float distanceFactor = Mathf.Clamp01(1 - (distance / explosionRadius));

                rb.AddForce(direction * explosionForce * distanceFactor, ForceMode.Impulse);

                if (distance < destroyEnemyDistance)
                {
                    if (nearby.CompareTag("enemigo"))
                    {
                        Destroy(nearby.gameObject);
                    }
                    else if (nearby.CompareTag("jefe"))
                    {
                        bossManager = nearby.gameObject.GetComponent<BossManagerScript>();
                        bossManager.vida += -100;
                    }
                }
            }
        }

    }
}