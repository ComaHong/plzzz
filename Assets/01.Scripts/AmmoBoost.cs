using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    // �����ͼ� ����� ������  ��ũ��Ʈ
    public Rifle rifle;
    // �������� ������ ����� �����Ŭ��
    public AudioClip ammoBoostSound;
    // ����� ������Ʈ
    public AudioSource audioSource;
    // �÷��̾� �ִϸ�����
    public Animator anim;

    // �������� ������ �Ҵ����� ü��
    private int magToGive = 30;
    // �������� ������ �˷� �� �ݰ� ����
    private float radius = 2.5f;

    private void Update()
    {
        // �÷��̾��� ��ġ�� �������� ������ ������ �ȿ� �������� �Ǻ��� if��
        if (Vector3.Distance(transform.position, rifle.transform.position) < radius)
        {
            // �������ȿ� ���԰� FŰ�� ������ 
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Open �ִϸ��̼� ���
                anim.SetBool("Open", true);
                // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ�� presentHealth�������� �������� ������ ��Ե� ü�� healthGive���� �Ҵ�.
                rifle.mag = magToGive;
                // healthBoostSoundŬ���� �ѹ����
                audioSource.PlayOneShot(ammoBoostSound);
                // ������ 1.5�ʵڿ� �ı�
                Destroy(gameObject, 1.3f);
            }
        }
    }
}
