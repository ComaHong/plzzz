using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camtest : MonoBehaviour
{
   
    public Transform cameraTransform; // 가상 카메라가 있는 오브젝트
    public float sensitivity = 10f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Y축 회전 제한 (예: -90도에서 90도 사이로 제한)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // X축 회전
        rotationY += mouseX;

        // 회전 적용
        cameraTransform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }
}
