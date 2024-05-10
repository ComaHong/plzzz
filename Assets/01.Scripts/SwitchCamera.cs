using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject AimCam; // �������� ����� ����ķ
    public GameObject AimCanvas; // ����ķ�� ����ϴ� ĵ����
    public GameObject ThirdPersonCam; // �÷��̾� 3��Ī ī�޶�
    public GameObject ThirdPersonCanvas; // �÷��̾� 3��Ī ī�޶� ��� ĵ����
    

    [Header("Camera Animator")]
    public Animator anim; // �÷��̾��� �ִϸ�����

    private void Start()
    {
        // �����ҋ� 3��Ī ī�޶� true ����ķ flase
        ThirdPersonCam.SetActive(true);
        ThirdPersonCanvas.SetActive(true);
        AimCam.SetActive(false);
        AimCanvas.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            //anim.SetBool("RifleWalk", true);
            anim.SetBool("Walk", true);

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

        }

        else if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            //anim.SetBool("RifleWalk", false);
            anim.SetBool("Walk", false);
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("IdleAim", false);
         //  anim.SetBool("RifleWalk", false);
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);


        }
    }
}


