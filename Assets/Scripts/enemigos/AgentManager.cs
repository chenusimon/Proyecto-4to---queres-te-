using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    public EnemyShoot enemyShoot;
    [SerializeField] NavMeshAgent agent;
    public Transform targetTR;
    [SerializeField] Animator anim;
    public float lookAngleY;
    public float lookAngleX;
    int cuenta = 0;
    int cuenta2 = 0;
    public bool activated = false;
    public Rigidbody rb;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;


        agent.enabled = false;

            
    }

    void Update()
    {
        if (agent.enabled == true)
        {
            agent.destination = targetTR.position;
            anim.SetFloat("speed", agent.velocity.magnitude);

            Vector3 direction = (targetTR.position - transform.position).normalized;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

                lookAngleY = lookRotation.eulerAngles.y;
                lookAngleX = lookRotation.eulerAngles.x;
            }
            if (cuenta >= 50)
            {
                Disparar();
                cuenta = 0;
            }
        }
    }

    void FixedUpdate()
    {
        cuenta = cuenta + 1;
        if (activated && agent.enabled == false)
        {
            cuenta2 += 1;
                if (cuenta < 250) 
                {
                    agent.enabled = true;
                    rb.isKinematic = true;
                cuenta2 = 0;
                }
        }
    }

    void Disparar()
    {
        enemyShoot.Shoot(lookAngleX, lookAngleY, 1);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            Destroy(gameObject);
        }
    }
    public void activate()
    {
        agent.enabled = true;
        activated = true;
    }
}