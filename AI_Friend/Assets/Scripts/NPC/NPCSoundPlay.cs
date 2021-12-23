using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSoundPlay : MonoBehaviour
{

    AudioSource introAudio;

    void Start()
    {
        introAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)

    {
        if (other.gameObject.CompareTag("Player"))
        {
            introAudio.Play();
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