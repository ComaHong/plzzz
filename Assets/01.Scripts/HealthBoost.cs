using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    // �����ͼ� ����� �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ
    public PlayerController playerController;
    // �������� ������ ����� �����Ŭ��
    public AudioClip healthBoostSound;
    // ����� ������Ʈ
    public AudioSource audioSource;
    // �÷��̾� �ִϸ�����
    public Animator anim;

    // �������� ������ �Ҵ����� ü��
    private float healthToGive = 120f;
    // �������� ���������� ������ �Ǵ��� ����
    private float radius = 2.5f;

    private void Update()
    {
        // �÷��̾��� ��ġ�� �������� ������ ������ �ȿ� �������� �Ǻ��� if��
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            // �������ȿ� ���԰� FŰ�� ������ 
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Open �ִϸ��̼� ���
                anim.SetBool("Open", true);
                // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� presentHealth�������� �������� ������ ��Ե� ü�� healthGive���� �Ҵ�.
                playerController.presentHealth = healthToGive;
                // healthBoostSoundŬ���� �ѹ����
                audioSource.PlayOneShot(healthBoostSound);
                // ������ 1.5�ʵڿ� �ı�
                //Destroy(gameObject, 5f);
            }
        }
    }
}
