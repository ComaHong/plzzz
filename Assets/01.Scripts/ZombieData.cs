using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName = "Zombie Data")]

public class ZombieData
{
     [Header("좀비 체력과 데미지")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;

    public float zombieSpeed; // 좀비 이동속도
    public float walkingpointRadius = 2f; // 좀비 이동할 반지름

}
