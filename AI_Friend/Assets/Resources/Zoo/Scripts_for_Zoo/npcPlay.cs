using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcPlay : MonoBehaviour
{

    private AudioSource introAudio;

    // Start is called before the first frame update
    void Start()
    {

        introAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision other)

    {

        if (other.gameObject.CompareTag("Player"))

        {

            print("colNpc");

            introAudio.Play();


            //other.gameObject.SetActive(false);

            //Destroy(other.gameObject);
            //Destroy(gameObject);

            //Debug.Log(count);

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))

        {
            introAudio.Stop();
        }
    }
}