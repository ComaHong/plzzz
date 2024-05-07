using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour
{
    public float objectHealth = 30f; // 오브젝트의 체력

    // 오브젝트가 피격을 받았을 때 호출되는 메서드
    public void ObjectHitDamage(float amount)
    {
        // 피격으로 인한 체력 감소
        objectHealth -= amount;
        // 체력이 0 이하로 떨어졌을 때
        if (objectHealth <= 0)
        {
            // 사망 처리 메서드 호출
            Die();
        }
    }

    // 오브젝트가 사망했을 때 호출되는 메서드
    private void Die()
    {
        // 오브젝트를 파괴합니다.
        Destroy(gameObject);
    }
}