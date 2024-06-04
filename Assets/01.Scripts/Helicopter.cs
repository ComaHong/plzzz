using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{
    public GameObject helicopter; // �︮���� ������Ʈ
    public PlayerController playerController; // �÷��̾� ��Ʈ�ѷ� ��ũ��Ʈ
    public Animation helicopterAnimation; // �︮���� �ִϸ�����
    public GameObject helicoptercam;
    public GameObject helicoptertext;
    [Header("������� �����͵�")]
    public GameObject isHelicoptercam;
    public GameObject AimCam;
    public GameObject Aimcanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject vihicleCam;
    public GameObject playerCharacter;
    public Transform helicopterDoor;
    


    public float forwardSpeed = 100f; // �︮������ Z�� �̵� �ӵ�
    public float descendSpeed = 10f; // �︮������ Y�� �ϰ� �ӵ�
    public float animationStopSpeed = 0.5f; // �ִϸ����� �ӵ��� ���̴� �ӵ�

    private Vector3 initialPosition = new Vector3(386.799988f, 161.699997f, -114f); // �︮������ �ʱ� ��ġ
    private Vector3 forwardTargetPosition = new Vector3(336.529999f, 161.699997f, 621f); // Z�� ��ǥ ��ġ
    private Vector3 finalTargetPosition = new Vector3(336.529999f, 17.7000008f, 621f); // ���� ��ǥ ��ġ
    private Vector3 leaveingPosition = new Vector3(386.799988f, 161.699997f, 2000.599976f); // ����Ⱑ ���������� ���ư� ��ġ

    private bool isMovingForward = false; // Z�� ���� �̵� ������ ����
    private bool isDescending = false; // Y�� ���� �ϰ� ������ ����
    private bool isSlowingDown = false; // �ִϸ����� �ӵ��� ���̴� ������ ����
    private bool isOpened = false;
    public  float radius = 100f; // �÷��̾� �ݰ� ��


    void Start()
    {
        transform.position = initialPosition; // �ʱ� ��ġ�� ����
        helicopter.SetActive(false); // �︮���� ��Ȱ��ȭ ���·� ����

    }

    private void Update()
    {
        // �÷��̾��� ��ġ���� ������ ��ġ�� ���̰��� radius ������ ������� ����Ǵ� ����
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            // ������ȣ�ۿ��ؽ�Ʈ����
            helicoptertext.SetActive(true);
            // ������ ž������ ���� ���¿��� FŰ�� ������ ������ ž��
            if (Input.GetKeyDown(KeyCode.F))
            {
                // ������ ���� �������� ����Ǵ� ����
                if (!isOpened)
                {
                    // ���� true
                    isOpened = true;
                    // radius �� �� �������� ������ �ֵ��� 5000���κ���
                    radius = 5000f;
                    Debug.Log("�︮���� ž��");
                    // ���� ��ȣ�ۿ��ؽ�Ʈ ��
                    helicoptertext.SetActive(false);
                   
                }
                // ������ ž���� ���¿��� FŰ�� ������ �������� ����

                // ������ ��쿡 ����� ����
                else
                {
                    // �÷��̾��� ��ġ���� ���� ������ ��ġ�� ����
                    playerController.transform.position = helicopterDoor.transform.position;
                    // ������
                    isOpened = false;
                    // radius�� 5�κ���
                    radius = 5f;
                    Debug.Log("�÷��̾� �︮���Ϳ��� ����");
                }
            }
        }
        // �������� ����������
        if (isOpened == true)
        {
            // 3��Īcam����
            ThirdPersonCam.SetActive(false);
            // 3��Īcanvas ����
            ThirdPersonCanvas.SetActive(false);
            // ����ķ ����
            AimCam.SetActive(false);
            // ����ĵ��������
            Aimcanvas.SetActive(false);
            // �ڵ��� �ó׸ӽŲ���
            vihicleCam.SetActive(false);
            // �÷��̾� ������Ʈ ����
            playerCharacter.SetActive(false);
            // ����ķ ����
            isHelicoptercam.SetActive(true);

            MoveHelicopter();



        }
        // ���� �����������
        //else if (isOpened == false)
        {
            // 3��Īī�޶� Ű��
            //ThirdPersonCam.SetActive(true);
            // 3��Īĵ���� Ű��
            //ThirdPersonCanvas.SetActive(true);
            // ����ķ ���
            //AimCam.SetActive(true);
            // ����ĵ���� ���
            //Aimcanvas.SetActive(true);
            // �÷��̾� ������Ʈ Ű��
            //playerCharacter.SetActive(true);
        }
    }



    // �︮���͸� Ȱ��ȭ�ϰ� �̵��� �����ϴ� �޼���
    public void ActivateHelicopter()
    {
        helicopter.SetActive(true); // �︮���� Ȱ��ȭ
        isMovingForward = true; // �︮���� Z�� �̵� ����
        StartHelicopter();
    }
    public void StartHelicopter()
    {
        if (isMovingForward)
        {
            // �︮���͸� Z�� �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, forwardTargetPosition, forwardSpeed * Time.deltaTime);

            // Z�� ��ǥ ��ġ�� �����ϸ� Z ���� �̵� �����ϰ� Y ���� �̵� ����
            if (transform.position == forwardTargetPosition)
            {
                isMovingForward = false;
                isDescending = true;
            }
        }

        if (isDescending)
        {
            // �︮���͸� Y�� �������� õõ�� �ϰ�
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, descendSpeed * Time.deltaTime);

            // ���� ��ǥ ��ġ�� �����ϸ� �̵� ����
            if (transform.position == finalTargetPosition)
            {
                isDescending = false;
                isSlowingDown = true;
            }
        }
        if (isSlowingDown)
        {
            // �ִϸ��̼��� �ӵ��� ���������� ���ҽ�Ŵ
            foreach (AnimationState state in helicopterAnimation)
            {
                state.speed = Mathf.Lerp(state.speed, 0, animationStopSpeed * Time.deltaTime);
                if (state.speed == 0)
                {
                    helicoptercam.SetActive(true);
                }
            }

        }
    


    }

    private void MoveHelicopter()
{
        helicopter.transform.position = Vector3.MoveTowards(transform.position, leaveingPosition, forwardSpeed * Time.deltaTime);
    }
}