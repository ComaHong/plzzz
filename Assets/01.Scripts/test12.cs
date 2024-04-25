using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test12 : MonoBehaviour
{
    public Rigidbody targetObject; // ȸ����ų ������Ʈ�� Rigidbody ������Ʈ
    public float rotationSpeed = 10f; // ȸ�� �ӵ�
    public float targetRotationAngle = 90f; // ��ǥ ȸ�� ����


    void Update ()
    {
        // ���� ȸ�� ����
        float currentRotationAngle = targetObject.rotation.eulerAngles.y;

        // ��ǥ ȸ�� �������� ����
        float angleDifference = Mathf.Abs(targetRotationAngle - currentRotationAngle);

        // ȸ�� �ӵ� ���� ����
        float torqueDirection = targetRotationAngle > currentRotationAngle ? 1f : -1f;

        // ȸ�� �ӵ��� ���� ���̿� ���� ȸ�� ��ũ�� �߰�
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (angleDifference > 1f) // ���� ���̰� ���� ������ ū ��쿡�� ȸ��
            {
                // Y������ ȸ�� ��ũ �߰�
                targetObject.AddTorque(Vector3.up * rotationSpeed * torqueDirection * Time.deltaTime, ForceMode.Force );
            }
            else // ���� ���̰� ���� �� ���ϸ� ȸ���� ���߰� ��ǥ ȸ�� ������ ����
            {
                // ȸ�� ��ũ �ʱ�ȭ
                targetObject.angularVelocity = Vector3.zero;
                // ��ǥ ȸ�� ������ ����
                targetObject.rotation = Quaternion.Euler(targetObject.rotation.eulerAngles.x, targetRotationAngle, targetObject.rotation.eulerAngles.z);
            }
        }
        
    }
}

   
    

