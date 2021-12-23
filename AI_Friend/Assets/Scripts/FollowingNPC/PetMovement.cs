using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


public class PetMovement : MonoBehaviour
{
    Transform player;
    private NavMeshAgent agent;

    public static PetMovement chName;
    public string ppName;

    private void Awake()
    {
        chName = this;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Invoke("Invoketest", 5f);
    }


    void Invoketest()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //player = GameObject.Find(ppName).GetComponent<Transform>();
    }


    private void Update()
    {
        if (player != null)
        {
            agent.destination = player.position;
        }
        else
        {
            Invoketest();
        }
    }
}