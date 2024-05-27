using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteController : MonoBehaviour
{
    public float descentSpeed = 0.5f; // ���� �ӵ�
    public float rotationSpeed = 20.0f; // ȸ�� �ӵ� (degrees per second)
    public float destroyDelay = 5.0f; // ���ϻ��� �����Ǳ� �� ��� �ð�

    void Start()
    {
        // ���ϻ� �ϰ� �� ȸ�� ����
        StartCoroutine(FallAndDestroy());
    }

    IEnumerator FallAndDestroy()
    {
        float elapsedTime = 0f;

        while (elapsedTime < destroyDelay)
        {
            // �ð� ���
            elapsedTime += Time.deltaTime;

            // ���ϻ� �ϰ�
            transform.Translate(Vector3.left * descentSpeed * Time.deltaTime, Space.World);

            // ���ϻ� ȸ��
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        // ���ϻ� ����
        Destroy(gameObject);
    }
}
