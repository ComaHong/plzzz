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
    public GameObject cartext; // 상호작용 텍스트
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
   public GameObject DestroyEffect; // 차량으로 파괴하면 재생할 이펙트 

    //public ParticleSystem muzzleSpark;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 위치값과 차량의 위치값 사이값이 radius 값보다 작을경우 실행되는 로직
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            // 차량상호작용텍스트켜짐
            cartext.SetActive(true);
            // 차량에 탑승하지 않은 상태에서 F키를 누르면 차량에 탑승
            if (Input.GetKeyDown(KeyCode.F))
            {
                // 차량의 문이 닫혓을떄 실행되는 로직
                if (!isOpened)
                {
                    // 열림 true
                    isOpened = true;
                    // radius 값 을 언제든지 내릴수 있도록 5000으로변경
                    radius = 5000f;
                    Debug.Log("차량 탑승");
                    // 차량 상호작용텍스트 끔
                    cartext.SetActive(false);
                    // ObjectivesComplete 스크립트의  GetobjectivesDone메서드의 매개변수값 변경
                    ObjectivesComplete.occurrence.GetobjectivesDone(true, true, true, false);
                }
                // 차량에 탑승한 상태에서 F키를 누르면 차량에서 내림

                // 나머지 경우에 실행될 로직
                else
                {
                    // 플레이어의 위치값을 차량 운전석 위치로 변경
                    playerController.transform.position = vehicleDoor.transform.position;
                    // 문닫힘
                    isOpened = false;
                    // radius값 5로변경
                    radius = 5f;
                    Debug.Log("플레이어 차량에서 내림");
                }
            }
        }
        // 차량문이 열려있으면
        if (isOpened == true)
        {
            // 3인칭cam끄기
            ThirdPersonCam.SetActive(false);
            // 3인칭canvas 끄기
            ThirdPersonCanvas.SetActive(false);
            // 에임캠 끄기
            AimCam.SetActive(false);
            // 에임캔버스끄기
            Aimcanvas.SetActive(false);
            // 플레이어 오브젝트 끄기
            PlayerCharacter.SetActive(false);
            // 차량캠 켜짐
            iscarcam.SetActive(true);

            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
            HitZombies();
        }
        // 문이 닫혀있을경우
        else if (isOpened == false)
        {
            // 3인칭카메라 키기
            ThirdPersonCam.SetActive(true);
            // 3인칭캔버스 키기
            ThirdPersonCanvas.SetActive(true);
            // 에임캠 사용
            AimCam.SetActive(true);
            // 에임캔버스 사용
            Aimcanvas.SetActive(true);
            // 플레이어 오브젝트 키기
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
        // 스페이스바로 차량브레이크 실행
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
    // 차량으로 좀비를 공격하는 메서드
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
