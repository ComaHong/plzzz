using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBarUI : MonoBehaviour
{
    // ī�޶��� ��ġ���� �޾ƿ� ����
    public Transform MainCamera;

    public void Awake()
    {
     
    }

    // LateUpdate �޼���� ��� ������Ʈ�� ���� �� ȣ��˴ϴ�.
    private void LateUpdate()
    {
        // HealthBar UI�� �׻� ���� ī�޶� ���ϵ��� �����մϴ�.
        // ��, HealthBar�� �׻� �÷��̾� ĳ���͸� ���� ȸ���մϴ�.
        // transform.position�� HealthBar�� ���� ��ġ�� ��Ÿ����,
        // MainCamera.forward�� ���� ī�޶��� ���� ������ ��Ÿ���ϴ�.
        // ���� LookAt �޼��带 ����Ͽ� HealthBar�� ���� ī�޶��� ���� �������� ȸ����ŵ�ϴ�.
        transform.LookAt(transform.position + MainCamera.forward);
    }
}
