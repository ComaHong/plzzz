using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camtest : MonoBehaviour
{
   
    public Transform cameraTransform; // ���� ī�޶� �ִ� ������Ʈ
    public float sensitivity = 10f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Y�� ȸ�� ���� (��: -90������ 90�� ���̷� ����)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // X�� ȸ��
        rotationY += mouseX;

        // ȸ�� ����
        cameraTransform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    }
}
