using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    // ����� ������ҽ�
    private AudioSource audioSource;

    // �߼Ҹ� ����� �ҽ�
    public AudioClip[] footstepsSound; 

    private void Awake()
    {
        // ������Ʈ�� �ʱ�ȭ�ϰ� ����� �ҽ��� ������
        audioSource = GetComponent<AudioSource>();
    }
    // ������ �� �Ҹ� Ŭ���� ��ȯ�ϴ� �޼���
    private AudioClip GetRandomFootStep()
    {
        // footstepsSound �迭���� ������ �ε����� �����Ͽ� �ش��ϴ� �� �Ҹ� Ŭ���� ��ȯ
        return footstepsSound[UnityEngine.Random.Range(0, footstepsSound.Length)];
    }
    // �߼Ҹ��� ����ϴ� �޼���
    private void Step()
    {
        // GetRandomFootStep �޼��带 ȣ���Ͽ� ������ �� �Ҹ� Ŭ���� ������
        AudioClip clip = GetRandomFootStep();
        // ������ �� �Ҹ� Ŭ���� ����� �ҽ����� ���
        audioSource.PlayOneShot(clip);
    }

   
}
