using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("발소리 소스")]
    public AudioClip[] footstepsSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private AudioClip GetRandomFootStep()
    {
       return footstepsSound[UnityEngine.Random.Range(0, footstepsSound.Length)];
    }

    private void Step()
    {
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }

   
}
