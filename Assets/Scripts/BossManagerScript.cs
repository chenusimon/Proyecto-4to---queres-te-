using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossManagerScript : MonoBehaviour
{
    public EnemyShoot EnemyShoot;
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

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
        targetTR = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
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

            if (cuenta >= 500)
            {
            Instantiate(misil, misilLaunch.position, misilLaunch.rotation);
            cuenta = 0;
            }
            if (cuenta2 >= 25)
        {
            EnemyShoot.Shoot(lookAngleX, (lookAngleY + 5), 1);
            cuenta2 = 0;
        }


    }

    void FixedUpdate()
    {
        cuenta += 1;
        cuenta2 += 1;
    }





    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            Destroy(gameObject);
        }
    }
}