using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCam : MonoBehaviour
{
    // 마우스 감도
    public float mouseSensitivity = 100f;

    // 상하 회전 제한 각도
    public float upperVerticalRotationLimit = 80f;
    public float lowerVerticalRotationLimit = -80f;

    // 수직 회전 각도
    private float verticalRotation = 0f;

    void Update()
    {
        // 마우스 입력 감지
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 수직 회전 적용 및 상하 회전 제한 각도 적용
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, lowerVerticalRotationLimit, upperVerticalRotationLimit);

        // 카메라 회전 적용
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}