using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject playerRifle; // �÷��̾ ���ϰ� �ִ� ������
    public GameObject PickupRifle; // �̸� ���� ���ִ� �Ⱦ� �� �� �ִ� ������
    public PlayerPunch playerPunch; // �÷��̾��� ��ġ
    public GameObject rifleUI; // �ѱ�UI

    [Header("Rifle Assign Things")]
    public PlayerController playerController; // ������ �� �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ
    public Animator anim; // �÷��̾� �ִϸ�����
    private float radius = 2.5f; // �÷��̾��� �ֺ��� üũ�� ������ ����
    private float nextTimeToPunch = 0f; // ������ġ���� �ð�
    public float punchCharge = 15f; // ��ġ������ �ð�

    private void Awake()
    {
        // ������ ���۵Ǹ� player�� Rifle�� ��
        playerRifle.SetActive(false);
        // rifleUI�� ��
        rifleUI.SetActive(false);
    }
    private void Update()
    {
        // ���콺 ���ʹ�ư�� ������ ���Ӿ��� �ð��� ������ġ�ð����� ���ų� ū ������ �Ѵ� ������ ��� ����� �ڵ�
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToPunch)
        {
            anim.SetBool("Punch", true);
            anim.SetBool("Idle", false);
            nextTimeToPunch = Time.time + 1f / punchCharge;
            playerPunch.Punch();
        }
        // ���� �ڵ� �̿��� ��� ��쿡 ����� �ڵ�
        else
        {
            anim.SetBool("Punch", false);
            anim.SetBool("Idle", true);
        }

        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerRifle.SetActive(true);
                PickupRifle.SetActive(false);
                // �Ҹ�

                // ������Ʈ 
                ObjectivesComplete.occurrence.GetobjectivesDone(true, false, false, false);
            }
        }
    }
}
