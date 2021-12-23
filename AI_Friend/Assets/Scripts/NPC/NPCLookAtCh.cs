using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLookAtCh : MonoBehaviour
{
    Transform player;
    public float findDistance = 8f;

    public static NPCLookAtCh chName;
    private string ppName;



    void Start()
    {
        chName = this;
        Invoke("Invoketest", 5f);
    }


    void Invoketest()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //player = GameObject.Find(ppName).GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        if (player != null)
        {
            if(Vector3.Distance(transform.position, player.position) < findDistance)
            {
                transform.LookAt(player);
                //transform.LookAt(new Vector3(player.position.x, player.position.y, transform.position.z), Vector3.up);
            }
        }
    }

}
