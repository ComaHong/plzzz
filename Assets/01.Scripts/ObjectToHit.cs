using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour
{
    public float objectHealth = 30f; // ������Ʈ�� ü��

    // ������Ʈ�� �ǰ��� �޾��� �� ȣ��Ǵ� �޼���
    public void ObjectHitDamage(float amount)
    {
        // �ǰ����� ���� ü�� ����
        objectHealth -= amount;
        // ü���� 0 ���Ϸ� �������� ��
        if (objectHealth <= 0)
        {
            // ��� ó�� �޼��� ȣ��
            Die();
        }
    }

    // ������Ʈ�� ������� �� ȣ��Ǵ� �޼���
    private void Die()
    {
        // ������Ʈ�� �ı��մϴ�.
        Destroy(gameObject);
    }
}