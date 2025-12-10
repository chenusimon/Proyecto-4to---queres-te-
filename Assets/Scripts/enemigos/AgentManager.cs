using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    public EnemyShoot GunDirection;
    [SerializeField] NavMeshAgent agent;
    public Transform targetTR;
    [SerializeField] Animator anim;
    public float lookAngleY;
    public float lookAngleX;
    int cuenta = 0;
    int cuenta2 = 0;
    int cuenta3 = 0;
    public int vida = 1;
    public Rigidbody rb;
    public bool grounded = false;
    public bool activated = false;
    float xRotation;
    float yRotation;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
        targetTR = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (activated)
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
                }

                if (cuenta2 >= 100)
                {
                    GunDirection.Shoot(xRotation, yRotation);
                    cuenta2 = 0;
                    Debug.Log(vida);
                }
            }
            xRotation = transform.eulerAngles.x;
            yRotation = transform.eulerAngles.y;
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

        if (cuenta3 >= 100 && grounded)
        {
            agent.enabled = true;
            rb.isKinematic = true;
            cuenta3 = 0;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("granadaArea")|| collision.gameObject.CompareTag("misilArea")|| collision.gameObject.CompareTag("bala"))
        {
            Destroy(gameObject);
        }
    }

    public void activate()
    {
        activated = true;
        agent.enabled = true;
    }
}