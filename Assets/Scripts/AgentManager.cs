using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    public DispararBala DispararBala;
    [SerializeField] NavMeshAgent agent;
    public Transform targetTR;
    [SerializeField] Animator anim;
    public float lookAngleY;
    public float lookAngleX;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;

        // Llamar a la función "Disparar" cada 1 segundo
        InvokeRepeating("Disparar", 1f, 1f);
    }

    void Update()
    {
        agent.destination = targetTR.position;
        anim.SetFloat("speed", agent.velocity.magnitude);

        // Dirección completa hacia el target (incluye altura)
        Vector3 direction = (targetTR.position - transform.position).normalized;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            // Guardamos los ángulos que ahora sí reflejan inclinación vertical también
            lookAngleY = lookRotation.eulerAngles.y;
            lookAngleX = lookRotation.eulerAngles.x;
        }
    }

        void Disparar()
    {
        DispararBala.Shoot(lookAngleX, lookAngleY);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            Destroy(gameObject);
        }
    }
}