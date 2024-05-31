using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("�� �ݶ��̴���")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backRightWheelCollider;
    public Transform vehicleDoor;


    [Header("�� ��ġ��")]
    public Transform frontLeftWheelTransform, frontRightWheelTransform;
    public Transform backLeftWheelTransform, backRightWheelTransform;
    [Header("�� ������ ����")]
    public float accelerationForce = 100f;
    public float breakingForce = 200f;
    public float presentAcceleration = 0f;
    private float presentBreakForce = 0f;

    [Header("���� ����")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("���� ��ȣ�ۿ�")]
    public PlayerController playerController;
    public GameObject cartext; // ��ȣ�ۿ� �ؽ�Ʈ
    private float radius = 5f;
    private bool isOpened = false;


    [Header("������� �����͵�")]
    public GameObject iscarcam;
    public GameObject AimCam;
    public GameObject Aimcanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject PlayerCharacter;

    [Header("�������� ����")]
    public Camera cam;
    public float hitRange = 2f;
    public float giveDamegeOf = 100f;
    public GameObject goreEffect;
   public GameObject DestroyEffect; // �������� �ı��ϸ� ����� ����Ʈ 

    //public ParticleSystem muzzleSpark;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾��� ��ġ���� ������ ��ġ�� ���̰��� radius ������ ������� ����Ǵ� ����
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            // ������ȣ�ۿ��ؽ�Ʈ����
            cartext.SetActive(true);
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
                    Debug.Log("���� ž��");
                    // ���� ��ȣ�ۿ��ؽ�Ʈ ��
                    cartext.SetActive(false);
                    // ObjectivesComplete ��ũ��Ʈ��  GetobjectivesDone�޼����� �Ű������� ����
                    ObjectivesComplete.occurrence.GetobjectivesDone(true, true, true, false);
                }
                // ������ ž���� ���¿��� FŰ�� ������ �������� ����

                // ������ ��쿡 ����� ����
                else
                {
                    // �÷��̾��� ��ġ���� ���� ������ ��ġ�� ����
                    playerController.transform.position = vehicleDoor.transform.position;
                    // ������
                    isOpened = false;
                    // radius�� 5�κ���
                    radius = 5f;
                    Debug.Log("�÷��̾� �������� ����");
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
            // �÷��̾� ������Ʈ ����
            PlayerCharacter.SetActive(false);
            // ����ķ ����
            iscarcam.SetActive(true);

            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
            HitZombies();
        }
        // ���� �����������
        else if (isOpened == false)
        {
            // 3��Īī�޶� Ű��
            ThirdPersonCam.SetActive(true);
            // 3��Īĵ���� Ű��
            ThirdPersonCanvas.SetActive(true);
            // ����ķ ���
            AimCam.SetActive(true);
            // ����ĵ���� ���
            Aimcanvas.SetActive(true);
            // �÷��̾� ������Ʈ Ű��
            PlayerCharacter.SetActive(true);
        }

    }
    // �������� ���� �ݶ��̴��� �ִ� motorTorque�� ������Ű�� �޼���
    private void MoveVehicle()
    {
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        frontRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * -Input.GetAxis("Vertical");
    }
    // ���� �̵��� ���� �̵��� ���� �� ������ �޼���
    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;

        // �� �ִϸ��̼�
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);

    }
    //  �� ������ �޼���
    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;
    }
    // ���� �극��ũ 
    private void ApplyBreaks()
    {
        // �����̽��ٷ� �����극��ũ ����
        if (Input.GetKey(KeyCode.Space))
        {
            presentBreakForce = breakingForce;
        }
        else
        {
            presentBreakForce = 0f;
        }
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;

    }
    // �������� ���� �����ϴ� �޼���
    void HitZombies()
    {
        RaycastHit hitinfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitinfo, hitRange))
        {
            Debug.Log(hitinfo.transform.name);

            Zombie1 zombie1 = hitinfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitinfo.transform.GetComponent<Zombie2>();
            ObjectToHit objectToHit = hitinfo.transform.GetComponent<ObjectToHit>();

            if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamegeOf);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(goreEffectGo, 0.5f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamegeOf);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(goreEffectGo, 0.5f);
            }

            else if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamegeOf);
                GameObject Woodgo = Instantiate(DestroyEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(Woodgo, 1f);
            }
        }
    }

}
