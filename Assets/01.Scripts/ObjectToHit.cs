using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour
{
    public float objectHealth = 30f;

    public void ObjectHitDamage(float amount)
    {
        objectHealth -= amount;
        if(objectHealth <= 0)
        {
            Die();
        }
     }

    private void Die()
    {
        Destroy(gameObject);
    }
}
