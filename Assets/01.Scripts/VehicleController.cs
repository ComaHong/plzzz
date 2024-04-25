using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("휠 콜라이더들")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backRightWheelCollider;
    public Transform vehicleDoor;


    [Header("휠 위치값")]
    public Transform frontLeftWheelTransform, frontRightWheelTransform;
    public Transform backLeftWheelTransform, backRightWheelTransform;
    [Header("차 엔진값 셋팅")]
    public float accelerationForce = 100f;
    public float breakingForce = 200f;
    public float presentAcceleration = 0f;
    private float presentBreakForce = 0f;

    [Header("차량 조종")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("차량 상호작용")]
    public PlayerController playerController;
    private float radius = 5f;
    private bool isOpened = false;


    [Header("사용하지 않을것들")]
    public GameObject iscarcam;
    public GameObject AimCam;
    public GameObject Aimcanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject PlayerCharacter;

    [Header("차량으로 공격")]
    public Camera cam;
    public float hitRange = 2f;
    public float giveDamegeOf = 100f;
    public GameObject goreEffect;
    //public ParticleSystem muzzleSpark;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpened = true;
                radius = 5000f;
                Debug.Log("차량 탑승");

            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                playerController.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;
                Debug.Log("플레이어 차량에서 내림");
            }
        }

        if (isOpened == true)
        {
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(false);
            Aimcanvas.SetActive(false);
            PlayerCharacter.SetActive(false);
            iscarcam.SetActive(true);

            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
            HitZombies();
        }
        else if (isOpened == false)
        {
            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(true);
            Aimcanvas.SetActive(true);
            PlayerCharacter.SetActive(true);
        }

    }
    // 엑셀힘을 차량 콜라이더에 있는 motorTorque에 누적시키는 메서드
    private void MoveVehicle()
    {
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        frontRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * -Input.GetAxis("Vertical");
    }
    // 차량 이동과 차량 이동에 따른 휠 굴리기 메서드
    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;

        // 휠 애니메이션
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);

    }
    //  휠 굴리기 메서드
    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;
    }
    // 차량 브레이크 
    private void ApplyBreaks()
    {

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
    void HitZombies()
    {
        RaycastHit hitinfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitinfo, hitRange))
        {
            Debug.Log(hitinfo.transform.name);

            Zombie1 zombie1 = hitinfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitinfo.transform.GetComponent<Zombie2>();

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
        }
    }

}
