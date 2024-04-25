using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCam : MonoBehaviour
{
    // ���콺 ����
    public float mouseSensitivity = 100f;

    // ���� ȸ�� ���� ����
    public float upperVerticalRotationLimit = 80f;
    public float lowerVerticalRotationLimit = -80f;

    // ���� ȸ�� ����
    private float verticalRotation = 0f;

    void Update()
    {
        // ���콺 �Է� ����
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ���� ȸ�� ���� �� ���� ȸ�� ���� ���� ����
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, lowerVerticalRotationLimit, upperVerticalRotationLimit);

        // ī�޶� ȸ�� ����
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}