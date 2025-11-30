using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossManagerScript : MonoBehaviour
{
    public GunDirection GunDirection;
    [SerializeField] NavMeshAgent agent;
    public Transform targetTR;
    [SerializeField] Animator anim;
    public float lookAngleY;
    public float lookAngleX;
    public GameObject misil;
    public bool isMoving = true;
    public Transform misilLaunch;
    int cuenta = 0;
    int cuenta2 = 0;
    int cuenta3 = 0;
    public int vida = 500;
    public Rigidbody rb;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
        targetTR = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (agent.enabled == true)
        {
            Debug.Log(vida);
            agent.destination = targetTR.position;
            anim.SetFloat("speed", agent.velocity.magnitude);

            Vector3 direction = (targetTR.position - transform.position).normalized;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

            if (cuenta >= 500)
            {
                Instantiate(misil, misilLaunch.position, misilLaunch.rotation);
                cuenta = 0;
            }
            if (cuenta2 >= 25)
            {
                GunDirection.Shoot();
                cuenta2 = 0;
            }
        }

    }

    void FixedUpdate()
    {
        cuenta += 1;
        cuenta2 += 1;
        if (agent.enabled == false) 
        {
            cuenta3 += 1;
        }

        if (cuenta3 >= 250)
        {
            agent.enabled = true;
            rb.isKinematic = true;
            cuenta3 = 0;
        }
    }

}