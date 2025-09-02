using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgenteCapsulaScript : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform targetTR;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = targetTR.position;
    }
}
