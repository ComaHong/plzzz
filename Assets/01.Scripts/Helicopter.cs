using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{
    public GameObject helicopter; // 헬리콥터 오브젝트
    public PlayerController playerController; // 플레이어 컨트롤러 스크립트
    public Animation helicopterAnimation; // 헬리콥터 애니메이터
    public GameObject helicoptercam;
    public GameObject helicoptertext;
    [Header("사용하지 않을것들")]
    public GameObject isHelicoptercam;
    public GameObject AimCam;
    public GameObject Aimcanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;
    public GameObject vihicleCam;
    public GameObject playerCharacter;
    public Transform helicopterDoor;
    


    public float forwardSpeed = 100f; // 헬리콥터의 Z축 이동 속도
    public float descendSpeed = 10f; // 헬리콥터의 Y축 하강 속도
    public float animationStopSpeed = 0.5f; // 애니메이터 속도를 줄이는 속도

    private Vector3 initialPosition = new Vector3(386.799988f, 161.699997f, -114f); // 헬리콥터의 초기 위치
    private Vector3 forwardTargetPosition = new Vector3(336.529999f, 161.699997f, 621f); // Z축 목표 위치
    private Vector3 finalTargetPosition = new Vector3(336.529999f, 17.7000008f, 621f); // 최종 목표 위치
    private Vector3 leaveingPosition = new Vector3(386.799988f, 161.699997f, 2000.599976f); // 비행기가 최종적으로 날아갈 위치

    private bool isMovingForward = false; // Z축 방향 이동 중인지 여부
    private bool isDescending = false; // Y축 방향 하강 중인지 여부
    private bool isSlowingDown = false; // 애니메이터 속도를 줄이는 중인지 여부
    private bool isOpened = false;
    public  float radius = 100f; // 플레이어 반경 값


    void Start()
    {
        transform.position = initialPosition; // 초기 위치로 설정
        helicopter.SetActive(false); // 헬리콥터 비활성화 상태로 시작

    }

    private void Update()
    {
        // 플레이어의 위치값과 차량의 위치값 사이값이 radius 값보다 작을경우 실행되는 로직
        if (Vector3.Distance(transform.position, playerController.transform.position) < radius)
        {
            // 차량상호작용텍스트켜짐
            helicoptertext.SetActive(true);
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
                    Debug.Log("헬리콥터 탑승");
                    // 차량 상호작용텍스트 끔
                    helicoptertext.SetActive(false);
                   
                }
                // 차량에 탑승한 상태에서 F키를 누르면 차량에서 내림

                // 나머지 경우에 실행될 로직
                else
                {
                    // 플레이어의 위치값을 차량 운전석 위치로 변경
                    playerController.transform.position = helicopterDoor.transform.position;
                    // 문닫힘
                    isOpened = false;
                    // radius값 5로변경
                    radius = 5f;
                    Debug.Log("플레이어 헬리콥터에서 내림");
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
            // 자동차 시네머신끄기
            vihicleCam.SetActive(false);
            // 플레이어 오브젝트 끄기
            playerCharacter.SetActive(false);
            // 차량캠 켜짐
            isHelicoptercam.SetActive(true);

            MoveHelicopter();



        }
        // 문이 닫혀있을경우
        //else if (isOpened == false)
        {
            // 3인칭카메라 키기
            //ThirdPersonCam.SetActive(true);
            // 3인칭캔버스 키기
            //ThirdPersonCanvas.SetActive(true);
            // 에임캠 사용
            //AimCam.SetActive(true);
            // 에임캔버스 사용
            //Aimcanvas.SetActive(true);
            // 플레이어 오브젝트 키기
            //playerCharacter.SetActive(true);
        }
    }



    // 헬리콥터를 활성화하고 이동을 시작하는 메서드
    public void ActivateHelicopter()
    {
        helicopter.SetActive(true); // 헬리콥터 활성화
        isMovingForward = true; // 헬리콥터 Z축 이동 시작
        StartHelicopter();
    }
    public void StartHelicopter()
    {
        if (isMovingForward)
        {
            // 헬리콥터를 Z축 방향으로 이동
            transform.position = Vector3.MoveTowards(transform.position, forwardTargetPosition, forwardSpeed * Time.deltaTime);

            // Z축 목표 위치에 도달하면 Z 방향 이동 종료하고 Y 방향 이동 시작
            if (transform.position == forwardTargetPosition)
            {
                isMovingForward = false;
                isDescending = true;
            }
        }

        if (isDescending)
        {
            // 헬리콥터를 Y축 방향으로 천천히 하강
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, descendSpeed * Time.deltaTime);

            // 최종 목표 위치에 도달하면 이동 중지
            if (transform.position == finalTargetPosition)
            {
                isDescending = false;
                isSlowingDown = true;
            }
        }
        if (isSlowingDown)
        {
            // 애니메이션의 속도를 점진적으로 감소시킴
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