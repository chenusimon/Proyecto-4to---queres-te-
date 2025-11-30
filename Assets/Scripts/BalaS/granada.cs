using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class granada : MonoBehaviour
{
    public GameObject explosionEffect;
    float explosionRadius = 10f;
    float explosionForce = 200f;
    float destroyEnemyDistance = 2f;
    public AgentManager agentManager;
    public BossManagerScript bossManager;
    public GameObject killradio;

    private bool hasExploded = false;

     void OnCollisionEnter(Collision collision)
     {
        if (collision.gameObject.CompareTag("Player")) return;

        Destroy(gameObject, 0.1f);
        if (hasExploded) return;
        hasExploded = true;
        

            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Instantiate(killradio, transform.position, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearby in colliders)
        {
            if ((nearby.gameObject.CompareTag("enemigo"))|| nearby.gameObject.CompareTag("jefe"))
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
            }
        }

     }

}
