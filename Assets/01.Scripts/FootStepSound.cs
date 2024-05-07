using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    // 사용할 오디오소스
    private AudioSource audioSource;

    // 발소리 오디오 소스
    public AudioClip[] footstepsSound; 

    private void Awake()
    {
        // 컴포넌트를 초기화하고 오디오 소스를 가져옴
        audioSource = GetComponent<AudioSource>();
    }
    // 랜덤한 발 소리 클립을 반환하는 메서드
    private AudioClip GetRandomFootStep()
    {
        // footstepsSound 배열에서 랜덤한 인덱스를 선택하여 해당하는 발 소리 클립을 반환
        return footstepsSound[UnityEngine.Random.Range(0, footstepsSound.Length)];
    }
    // 발소리를 재생하는 메서드
    private void Step()
    {
        // GetRandomFootStep 메서드를 호출하여 랜덤한 발 소리 클립을 가져옴
        AudioClip clip = GetRandomFootStep();
        // 가져온 발 소리 클립을 오디오 소스에서 재생
        audioSource.PlayOneShot(clip);
    }

   
}
