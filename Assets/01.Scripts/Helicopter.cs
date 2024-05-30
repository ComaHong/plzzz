using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public GameObject helicopter; // 헬리콥터 오브젝트
    public PlayerController playerController; // 플레이어 컨트롤러 스크립트
    public Animation helicopterAnimation; // 헬리콥터 애니메이터
    public float forwardSpeed = 100f; // 헬리콥터의 Z축 이동 속도
    public float descendSpeed = 10f; // 헬리콥터의 Y축 하강 속도
    public float animationStopSpeed = 0.5f; // 애니메이터 속도를 줄이는 속도

    private Vector3 initialPosition = new Vector3(386.799988f, 161.699997f, -114f); // 헬리콥터의 초기 위치
    private Vector3 forwardTargetPosition = new Vector3(336.529999f, 161.699997f, 621f); // Z축 목표 위치
    private Vector3 finalTargetPosition = new Vector3(336.529999f, 20.1599998f, 621f); // 최종 목표 위치

    private bool isMovingForward = false; // Z축 방향 이동 중인지 여부
    private bool isDescending = false; // Y축 방향 하강 중인지 여부
    private bool isSlowingDown = false; // 애니메이터 속도를 줄이는 중인지 여부
    private float radius = 5f; // 플레이어 반경 값

    void Start()
    {
        transform.position = initialPosition; // 초기 위치로 설정
        helicopter.SetActive(false); // 헬리콥터 비활성화 상태로 시작
       
    }

    
   

    // 헬리콥터를 활성화하고 이동을 시작하는 메서드
    public void ActivateHelicopter()
    {
        helicopter.SetActive(true); // 헬리콥터 활성화
        isMovingForward = true; // 헬리콥터 Z축 이동 시작
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
            }

        }
    }
}