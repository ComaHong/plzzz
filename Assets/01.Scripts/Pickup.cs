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
            // ��ġ �ִϸ��̼� ���
            anim.SetBool("Punch", true);
            // ��ġ �ִϸ��̼� ����
            anim.SetBool("Idle", false);
            // ���� ��ġ�ð� ����
            nextTimeToPunch = Time.time + 1f / punchCharge;
            // playerPunch��ũ��Ʈ�� Punch�޼��� ȣ��
            playerPunch.Punch();
        }
        // ���� �ڵ� �̿��� ��� ��쿡 ����� �ڵ�
        else
        {
            // ��ġ �ִϸ��̼� ����
            anim.SetBool("Punch", false);
            // Idle�ִϸ��̼� ����
            anim.SetBool("Idle", true);
        }
        // �÷��̾�� �� ��ũ��Ʈ�� ������ ���� ������Ʈ ���� �Ÿ��� Ư�� �ݰ� �̳����� Ȯ���մϴ�.
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            // �÷��̾ ���� �ֿ� �� �ִ� ���� ���� �ְ� F Ű�� ������ �Ʒ� �ڵ尡 ����˴ϴ�.
            if (Input.GetKeyDown(KeyCode.F))
            {
                // �÷��̾��� �� ������Ʈ�� Ȱ��ȭ
                playerRifle.SetActive(true);
                // �Ⱦ��� ���� ������Ʈ�� ��Ȱ��ȭ
                PickupRifle.SetActive(false);
                // �Ҹ�

                // ����ó�� �� objectiveComplete��ũ��Ʈ�� GetobjectivesDone�޼��带 ���� ù��° ��ǥ�� true�� ���� ������ ��ǥ���� �״�� false
                ObjectivesComplete.occurrence.GetobjectivesDone(true, false, false, false);
            }
        }
    }
}
