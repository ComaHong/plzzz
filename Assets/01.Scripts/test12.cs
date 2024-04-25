using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test12 : MonoBehaviour
{
    public Rigidbody targetObject; // 회전시킬 오브젝트의 Rigidbody 컴포넌트
    public float rotationSpeed = 10f; // 회전 속도
    public float targetRotationAngle = 90f; // 목표 회전 각도


    void Update ()
    {
        // 현재 회전 각도
        float currentRotationAngle = targetObject.rotation.eulerAngles.y;

        // 목표 회전 각도와의 차이
        float angleDifference = Mathf.Abs(targetRotationAngle - currentRotationAngle);

        // 회전 속도 방향 설정
        float torqueDirection = targetRotationAngle > currentRotationAngle ? 1f : -1f;

        // 회전 속도와 각도 차이에 따라 회전 토크를 추가
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (angleDifference > 1f) // 각도 차이가 일정 값보다 큰 경우에만 회전
            {
                // Y축으로 회전 토크 추가
                targetObject.AddTorque(Vector3.up * rotationSpeed * torqueDirection * Time.deltaTime, ForceMode.Force );
            }
            else // 각도 차이가 일정 값 이하면 회전을 멈추고 목표 회전 각도로 설정
            {
                // 회전 토크 초기화
                targetObject.angularVelocity = Vector3.zero;
                // 목표 회전 각도로 설정
                targetObject.rotation = Quaternion.Euler(targetObject.rotation.eulerAngles.x, targetRotationAngle, targetObject.rotation.eulerAngles.z);
            }
        }
        
    }
}

   
    

